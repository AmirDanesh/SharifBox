import { VerificationCodeComponent } from './../verification-code/verification-code.component';
import { LocalStorageService } from 'ngx-webstorage';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { ToastComponent } from 'src/app/shared/components/toast/toast.component';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
  signupForm;
  phoneNumber!: string;
  horizontalPosition: MatSnackBarHorizontalPosition = 'start';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';

  constructor(private authService: AuthService, private localstorage: LocalStorageService, private dialog: MatDialog, private _snackBar: MatSnackBar) {
    const phoneNumberPattern = '^09\\d{9}$';



    this.signupForm = new FormGroup({
      firstName: new FormControl(null, [Validators.required, Validators.pattern('^[a-zA-Zآ-ی ء چ]+$')]),
      lastName: new FormControl(null, [Validators.required, Validators.pattern('^^[a-zA-Zآ-ی ء چ]+$')]),
      phoneNumber: new FormControl(null, [Validators.required, Validators.pattern(phoneNumberPattern)]),
    });
  }

  ngOnInit(): void {
  }

  signup() {
    this.signupForm.markAllAsTouched();
    if (!this.signupForm.valid)
      return;
    this.authService.signup(this.signupForm.value)
      .subscribe(() => {
        this.phoneNumber = this.signupForm.get('phoneNumber')?.value;
        this.localstorage.store('phoneNumber', this.phoneNumber)
        this.localstorage.store('fullName', this.signupForm.get('firstName')?.value + " " + this.signupForm.get('lastName')?.value)
        this.localstorage
        this._snackBar.openFromComponent(ToastComponent, {
          data: {
            text: 'درخواست با موفقیت انجام شد'
          }
        });
        this.openCodeDialog()
      }, err => this._snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
      );
  }



  openCodeDialog() {
    const dialogRef = this.dialog.open(VerificationCodeComponent, {
      height: '400px',
      width: '600px',
    });
    dialogRef.afterClosed().subscribe((result) => { });
  }
}
