import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators, ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { LocalStorageService, SessionStorageService } from 'ngx-webstorage';
import { Router } from '@angular/router';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { ToastComponent } from 'src/app/shared/components/toast/toast.component';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})

export class AuthComponent implements OnInit {

  colors = ['cl1', 'cl2', 'cl3', 'cl4', 'cl5', 'cl6', 'cl7', 'cl8', 'cl9'];
  signupForm: FormGroup;
  loginForm: FormGroup;
  forgotPasswordForm: FormGroup;
  codeForm: FormGroup;
  passwordForm: FormGroup;
  transformValue = '';
  phoneNumber!: string;
  horizontalPosition: MatSnackBarHorizontalPosition = 'start';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';

  // tslint:disable-next-line: max-line-length
  constructor(
    private localstorage: LocalStorageService,
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar) {
    const phoneNumberPattern = '^09\\d{9}$';

    this.signupForm = new FormGroup({
      firstName: new FormControl(null, [Validators.required, Validators.pattern('^[a-zA-Zآ-ی ء چ]+$')]),
      lastName: new FormControl(null, [Validators.required, Validators.pattern('^[a-zA-Zآ-ی ء چ]+$')]),
      phoneNumber: new FormControl(null, [Validators.required, Validators.pattern(phoneNumberPattern)]),
    });
    this.loginForm = new FormGroup({
      username: new FormControl(null, [Validators.required, Validators.pattern(phoneNumberPattern)]),
      password: new FormControl(null, [Validators.required])
    });

    this.forgotPasswordForm = new FormGroup({
      phoneNumber: new FormControl(null, [Validators.required, Validators.pattern(phoneNumberPattern)])
    });

    this.codeForm = new FormGroup({
      phoneNumber: new FormControl(),
      verificationCode: new FormControl(null, [Validators.required, Validators.pattern('^[0-9]{6}$')])
    });

    this.passwordForm = new FormGroup({
      password: new FormControl(null, [Validators.required, Validators.pattern('(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,50})$')]),
      passwordConfirm: new FormControl(null, Validators.required),
    });
  }

  ngOnInit(): void { }

  //first stage of signup
  showSignup(): void {
    this.transformValue = 'translateZ(-100px) rotateY(-90deg)';
    console.log('im here')
  }

  signup(): void {
    this.signupForm.markAllAsTouched();
    if (!this.signupForm.valid) {
      return;
    }

    this.authService.signup(this.signupForm.value)
      .subscribe(() => {
        this.phoneNumber = this.signupForm.get('phoneNumber')?.value;
        this.localstorage.store('phoneNumber', this.phoneNumber);
        this.localstorage.store('fullName', this.signupForm.get('firstName')?.value + ' ' + this.signupForm.get('lastName')?.value);
        this.snackBar.openFromComponent(ToastComponent, {
          data: {
            text: 'درخواست با موفقیت انجام شد'
          }
        });

        this.showCode();
      }, err => this.snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
      );
  }

  //second stage of signup
  showCode(): void {
    this.transformValue = 'translateZ(-100px) rotateX( -90deg)';
  }

  submitCode(): void {
    this.codeForm.markAllAsTouched();
    if (!this.codeForm.valid) {
      return;
    }

    this.codeForm.patchValue(
      { phoneNumber: this.phoneNumber, verificationCode: this.codeForm.get('verificationCode')?.value });
    this.authService.submitCode(this.codeForm.value)

      .subscribe((nxt: any) => {
        console.log(this.codeForm.value)
        this.showPassword();
        this.localstorage.store('utid', nxt.token);
        this.snackBar.openFromComponent(ToastComponent, {
          data: {
            text: 'درخواست با موفقیت انجام شد'
          }

        });
      }, err => this.snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
      );
  }

  //third stage of signup
  showPassword(): void {
    this.transformValue = 'translateZ(-100px) rotateY( 90deg)';
  }

  submitPass(): void {
    this.passwordForm.markAllAsTouched();
    if (!this.passwordForm.valid) {
      return;
    }

    this.authService.submitPass(
      {
        phoneNumber: this.phoneNumber,
        password: this.passwordForm.controls.password.value
      })
      .subscribe(nxt => {
        console.log(nxt)
        this.localstorage.store('uid', nxt);
        this.router.navigate(['profile']);

        this.snackBar.openFromComponent(ToastComponent, {
          data: {
            text: 'درخواست با موفقیت انجام شد'
          }
        });
      }, err => this.snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
      );
  }

  showLogin(): void {
    this.transformValue = 'translateZ(-100px)';
  }

  login(): void {
    this.loginForm.markAllAsTouched();
    if (!this.loginForm.valid) {
      return;
    }

    this.authService.login(this.loginForm.value)
      .subscribe((nxt: any) => {
        this.localstorage.store('utid', nxt.token);
        this.localstorage.store('fullName', nxt.fullName);
        this.localstorage.store('uid', nxt.id);
        this.phoneNumber = this.loginForm.get('username')?.value;
        this.localstorage.store('phoneNumber', this.phoneNumber);

        this.router.navigate(['profile']);
        this.authService.userSubject.next({ fullName: nxt.fullName })
      }, err => this.snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
      );
  }

  forgetPass(): void {
    this.authService.forgotPass(this.forgotPasswordForm.value).subscribe(
      (nxt: any) => {
        this.phoneNumber = this.forgotPasswordForm.get('phoneNumber')?.value;

        console.log(nxt);
        this.showCode();
      }, err => this.snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
    );
  }

  showForgotPassword(): void {
    this.transformValue = 'translateZ(-100px) rotateY( -180deg)';
  }

  showThankYou(): void {
    this.transformValue = 'translateZ(-100px) rotateX( 90deg)';
  }
}
