import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { LocalStorageService } from 'ngx-webstorage';
import { AuthService } from './../../../shared/services/auth.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ToastComponent } from 'src/app/shared/components/toast/toast.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-pass',
  templateUrl: './change-pass.component.html',
  styleUrls: ['./change-pass.component.scss']
})
export class ChangePassComponent implements OnInit {
  passwordForm: FormGroup;
  horizontalPosition: MatSnackBarHorizontalPosition = 'start';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';

  constructor(private authService: AuthService,
    private localStorage: LocalStorageService,
    private _snackBar: MatSnackBar,
    private router: Router,
  ) {
    this.passwordForm = new FormGroup({
      currentPassword: new FormControl(null, [Validators.required, Validators.pattern('(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$')]),
      newPassword: new FormControl(null, [Validators.required, Validators.pattern('(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$')]),
      passwordConfirm: new FormControl(null, Validators.required),
    });
  }

  ngOnInit(): void {
  }


  submitPass(): void {
    this.passwordForm.markAllAsTouched();
    if (!this.passwordForm.valid) { return; }

    this.authService.changePass({ currentPassword: this.passwordForm.controls.currentPassword.value, newPassword: this.passwordForm.controls.newPassword.value })
      .subscribe(nxt => {
        this.localStorage.store('uid', nxt);
        this._snackBar.openFromComponent(ToastComponent, {
          data: {
            text: 'درخواست با موفقیت انجام شد'
          }
        });
        this.passwordForm.reset()
        this.router.navigate(['profile']);

      }, err => this._snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
      );
  }

}
