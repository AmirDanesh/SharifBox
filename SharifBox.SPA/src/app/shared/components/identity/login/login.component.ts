import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { LocalStorageService } from 'ngx-webstorage';
import { MatDialog } from '@angular/material/dialog';
import { ToastComponent } from 'src/app/shared/components/toast/toast.component';
import { AuthService } from 'src/app/shared/services/auth.service';
import { MatStepper } from '@angular/material/stepper';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class loginComponent implements OnInit {
  loginForm: FormGroup;
  horizontalPosition: MatSnackBarHorizontalPosition = 'start';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';
  @Output() next = new EventEmitter<string>();

  constructor(private authService: AuthService, private localstorage: LocalStorageService, private router: Router, private _snackBar: MatSnackBar, public dialog: MatDialog) {
    const phoneNumberPattern = '^09\\d{9}$';
    this.loginForm = new FormGroup({
      username: new FormControl(null, [Validators.required, Validators.pattern(phoneNumberPattern)]),
      password: new FormControl(null, [Validators.required, Validators.pattern('(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$')])
    });
  }

  ngOnInit(): void {
  }

  login() {
    this.authService.login(this.loginForm.value)
      .subscribe((nxt: any) => {
        this.localstorage.store('utid', nxt.token);
        this.localstorage.store('fullName', nxt.fullName);
        this.localstorage.store('uid', nxt.id);
        // this.router.navigate(['/payment']);

      }, err => this._snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
      )
  }


  showSignup() {
    // const dialogRef = this.dialog.open(SignupComponent, {
    //   height: '400px',
    //   width: '600px',
    // });
    // dialogRef.afterClosed().subscribe((result) => { });
    this.next.emit();
  }


}
