import { AuthService } from './../../shared/services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DropdownModel } from 'src/app/shared/models/dropdown.model';
import { SkillsService } from 'src/app/shared/services/skills.service';
import { UserService } from 'src/app/shared/services/user.service';
import { LocalStorageService } from 'ngx-webstorage';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { ToastComponent } from 'src/app/shared/components/toast/toast.component';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  skillsDropDown: DropdownModel[] = [];
  // imageDefault: string = 'https://randomuser.me/api/portraits/women/79.jpg';
  horizontalPosition: MatSnackBarHorizontalPosition = 'start';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';
  imageExists = false;
  addTagFn: ((value: any) => Promise<DropdownModel>) | boolean = false;
  uid: any;
  // form
  profileForm: FormGroup = new FormGroup({});
  id: any;
  constructor(
    private userService: UserService,
    private skillsService: SkillsService,
    private route: ActivatedRoute,
    private localstorage: LocalStorageService,
    private _snackBar: MatSnackBar,
    private authService: AuthService) {

    this.id = this.localstorage.retrieve('phoneNumber');
    this.uid = this.localstorage.retrieve('uid');
    console.log(this.id);
  }

  ngOnInit(): void {
    // ngselect
    this.profileForm = new FormGroup({
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      nationalCode: new FormControl(null, Validators.required),
      address: new FormControl(null, Validators.required),
      skillIds: new FormControl(null, Validators.required),
      imageProfile: new FormControl(`http://demo.borhansoft.ir:5080/profilePicture/${this.uid}`)
    });
    this.skillsService.getSkills().subscribe(nxt => this.skillsDropDown = nxt);

    this.addTagFn = async (name) => {
      return { ...await this.skillsService.addSkill(name).toPromise(), tag: true };
    };

    this.getSelectedUser();


  }



  showPreview(event: any) {
    const file = (event.target).files[0];
    this.profileForm.patchValue({
      avatar: file
    });
    this.profileForm.get('avatar')?.updateValueAndValidity();

    // File Preview
    const reader = new FileReader();
    reader.onload = () => {
      // this.imageURL = reader.result as string;
      this.profileForm.patchValue({ imageProfile: reader.result });
    };
    reader.readAsDataURL(file);
  }

  getSelectedUser() {
    this.userService.getSelectedUser(this.id).subscribe(
      (nxt: any) => {
        this.profileForm.patchValue(nxt);
        console.log(nxt);
      }
    );
  }



  // form
  submitProfile() {
    this.profileForm.markAllAsTouched();
    if (!this.profileForm.valid) {
      return;
    }

    this.userService.submitProfile(this.profileForm.value)
      .subscribe(nxt => {
        this._snackBar.openFromComponent(ToastComponent, {
          data: {
            text: 'درخواست با موفقیت انجام شد'
          }
        });
      //  this.profileForm.reset();
      }, err => this._snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      }));
  }
}
