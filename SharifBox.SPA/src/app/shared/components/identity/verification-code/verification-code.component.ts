import { Router } from '@angular/router';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { LocalStorageService } from 'ngx-webstorage';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastComponent } from 'src/app/shared/components/toast/toast.component';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-verification-code',
  templateUrl: './verification-code.component.html',
  styleUrls: ['./verification-code.component.scss']
})
export class VerificationCodeComponent implements OnInit {
  codeForm;
  horizontalPosition: MatSnackBarHorizontalPosition = 'start';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';
  phoneNumber: any;

  constructor(private authService: AuthService,
    private localstorage: LocalStorageService,
    private _snackBar: MatSnackBar,
    private router: Router) {

    this.codeForm = new FormGroup({
      phoneNumber: new FormControl(),
      verificationCode: new FormControl(null, [Validators.required, Validators.pattern('^[0-9]{6}$')])
    });
  }

  ngOnInit(): void { }

  submitCode(): void {
    this.codeForm.markAllAsTouched();
    if (!this.codeForm.valid) { return; }
    this.phoneNumber = this.localstorage.retrieve('phoneNumber')
    this.codeForm.patchValue({ phoneNumber: this.phoneNumber, verificationCode: this.codeForm.get('verificationCode')?.value })
    this.authService.submitCode(this.codeForm.value)
      .subscribe((nxt: any) => {
        this.localstorage.store('utid', nxt.token);
        this._snackBar.openFromComponent(ToastComponent, {
          data: {
            text: 'درخواست با موفقیت انجام شد'
          }
        });
        this.router.navigate(['/payment'])
      }, err => this._snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
      );
  }

}
