import { UserService } from 'src/app/shared/services/user.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { DropdownModel } from 'src/app/shared/models/dropdown.model';
import { TeamService } from 'src/app/shared/services/team.service';
import { map } from 'rxjs/operators';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { ToastComponent } from 'src/app/shared/components/toast/toast.component';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.scss']
})
export class TeamComponent implements OnInit {
  teamUsers: any[] = [];
  teamManager: any = {};
  teamForm: FormGroup;
  imageURL = '';
  userName = '';
  id = '';
  editMode = false;
  userDropDown: DropdownModel[] = [];
  horizontalPosition: MatSnackBarHorizontalPosition = 'start';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';

  constructor(
    private teamService: TeamService,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private _snackBar: MatSnackBar) {
    this.teamForm = new FormGroup({
      id: new FormControl(null),
      name: new FormControl(null, Validators.required),
      managerUserId: new FormControl(null, Validators.required),
      users: new FormControl(),
      activityField: new FormControl(null),
      description: new FormControl(null),
      teamLogo: new FormControl({}),
      selectedUser: new FormControl()
    });

  }

  userImageUrl(userId: string) {
    return `http://demo.borhansoft.ir:5080/profilepicture/${userId}`;
  }

  ngOnInit(): void {
    this.route.params.subscribe(
      (params: Params) => {
        this.id = params.id;
        console.log(this.id)
        if (this.id) {
          this.editMode = true;
          this.teamForm.controls.teamLogo = new FormControl(`http://demo.borhansoft.ir:5080/teamLogo/${this.id}`);
        } else {
          this.teamForm.controls.teamLogo = new FormControl('/assets/images/Profile_avatar_placeholder_large (1).png');
        }
      }
    );

    this.getSelectedTeam();

    this.userService.getUserList().subscribe(nxt => {
      this.userDropDown = nxt;
    }
    );
  }


  // image file
  showPreview(event: any) {
    const file = (event.target).files[0];
    this.teamForm.patchValue({
      avatar: file
    });
    this.teamForm.get('avatar')?.updateValueAndValidity();

    // File Preview
    const reader = new FileReader();
    reader.onload = () => {
      // this.imageURL = reader.result as string;
      this.teamForm.patchValue({ teamLogo: reader.result });
    };
    reader.readAsDataURL(file);
    console.log(this.teamForm.get('teamLogo')?.value);
  }


  // post
  addTeam() {

    this.teamForm.patchValue({ users: this.teamUsers.map(x => x.id) })
    console.log(this.teamForm.value);
    this.teamService.addTeam(this.teamForm.value).subscribe(
      nxt => {
        this.router.navigate(['team', 'list']);
        this._snackBar.openFromComponent(ToastComponent, {
          data: {
            text: 'درخواست با موفقیت انجام شد'
          }
        });
        console.log(nxt)
      }
      , err => this._snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
    );
  }

  // put
  updateTeam() {
    this.teamForm.patchValue({ id: this.id })
    this.teamForm.patchValue({ users: this.teamUsers.map(x => x.id) })
    this.teamService.updateTeam(this.teamForm.value, this.id)
      .subscribe(
        nxt => {
          console.log(nxt)
          this.teamForm.patchValue(nxt);
          this.router.navigate(['team', 'list']);
          this._snackBar.openFromComponent(ToastComponent, {
            data: {
              text: 'درخواست با موفقیت انجام شد'
            }
          });
        },
        err => this._snackBar.openFromComponent(ToastComponent, {
          data: {
            text: err.error
          }
        })
      );
  }


  submitTeam() {
    this.teamForm.markAllAsTouched();
    if (!this.teamForm.valid) { return; }
    if (this.editMode) {
      this.updateTeam();
    } else {
      this.addTeam();
    }
  }

  // add user by dropdown
  addUserdd(event: any) {
    this.teamUsers.push(event);
    this.teamForm.get('selectedUser')?.reset();
  }

  // delete user from deropdown
  onDelete(event: any) {
    const index = this.teamUsers.findIndex(item => item.id === event);
    if (index > -1) {
      this.teamUsers.splice(index, 1);
    }
  }


  getSelectedTeam() {
    if (this.editMode) {
      this.teamService.getSelectedTeam(this.id).pipe(
        map((res: any) => {
          this.teamUsers = (res.users as any[]).map(u => { return { id: u.key, name: u.value } });
          // this.teamManager = res.managerUser;
          res.managerUserId = res.managerUser.key;
          res.users = res.users.map((x: any) => x.key);
          return res;
        }))
        .subscribe(
          nxt => {
            this.teamForm.patchValue(nxt);
          });


      // get image
      this.imageURL = `http://demo.borhansoft.ir:5080/teamLogo/${this.id}`;

    }
  }


}
