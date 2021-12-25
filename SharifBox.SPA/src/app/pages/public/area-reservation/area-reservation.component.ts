import { AuthService } from './../../../shared/services/auth.service';
import { Router } from '@angular/router';
import { LocalStorageService } from 'ngx-webstorage';
import { AreaService } from './../../../shared/services/area.service';
import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastComponent } from 'src/app/shared/components/toast/toast.component';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatStepper } from '@angular/material/stepper';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { PaymentAreaModel } from 'src/app/shared/models/space/paymentArea.model';


@Component({
  selector: 'app-area-reservation',
  templateUrl: './area-reservation.component.html',
  styleUrls: ['./area-reservation.component.scss']
})
export class AreaReservationComponent implements OnInit {
  @Input() svgId = '';
  model: any | undefined;
  reserveModel: any = {};
  timeModel: any;
  // invoiceModel: InvoiceAreaModel | undefined;
  invoiceModel: any;
  username: any;
  user: any;
  // svgIds = ['svg_1', 'svg_2', 'svg_3', 'svg_4']
  @ViewChild('stepper') stepper: MatStepper;
disable:any;
data:any;
  // login
  loginForm: FormGroup;
  horizontalPosition: MatSnackBarHorizontalPosition = 'start';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';
  // @Output() next = new EventEmitter<string>();

  //signup
  signupForm;
  phoneNumber!: string;

  //code
  codeForm;

  //payment
  paymentModel: PaymentAreaModel | undefined;

  constructor(private areaService: AreaService,
    private localStorage: LocalStorageService,
    private router: Router,
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private authService:AuthService
    ) {

      const phoneNumberPattern = '^09\\d{9}$';

  // login
      this.loginForm = new FormGroup({
        username: new FormControl(null, [Validators.required, Validators.pattern(phoneNumberPattern)]),
        password: new FormControl(null, [Validators.required, Validators.pattern('(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$')])
      })

  //signup
    this.signupForm = new FormGroup({
      firstName: new FormControl(null, [Validators.required, Validators.pattern('^[a-zA-Z]+$')]),
      lastName: new FormControl(null, [Validators.required, Validators.pattern('^[a-zA-Z]+$')]),
      phoneNumber: new FormControl(null, [Validators.required, Validators.pattern(phoneNumberPattern)]),
    });

  //code
    this.codeForm = new FormGroup({
      phoneNumber: new FormControl(),
      verificationCode: new FormControl(null, [Validators.required, Validators.pattern('^[0-9]{6}$')])
    });
    }

  ngOnInit(): void {
    this.username = this.reserveModel.userName;
    //this.username = this.localStorage.retrieve('phoneNumber');
    this.user = this.authService.isLoggedIn();
    console.log('user ='+ this.user)

    // this.signup=this.fb.group({
    //   name:[,Validators.required]
    // })

    // this.model = this.locationService.getState() as InvoiceAreaModel;
  }

  nextStep(){
    this.stepper.next();
  }



//date
  reserveDate() {
    this.timeModel = {
      startDate: this.reserveModel.startDate,
      endDate: this.reserveModel.endDate,
      userName: this.username,
    }
    // console.log(this.timeModel)
    // this.nextStep()
    //  this.areaService.reserveTime(this.timeModel).subscribe(
    //    (nxt:any) =>{
    //     // this.disable =nxt[0].disable;
    //       // status = nxt[0].status;
    //       this.data = nxt;
    //       console.log(nxt)
    //     // @ViewChild('stepper') stepper: MatStepper;

    //    }
    //  )
  }


//area
 // get selected area by clicking on the area
 getSelectedArea(event: any) {
  //  if(!this.data[0].disable){
  //    console.log(this.data.disable)
  this.svgId = event;
  this.areaService.getSelectedArea(this.svgId).subscribe(
    nxt => {
      this.model = nxt;

      // this._snackBar.openFromComponent(ToastComponent,{
      //   data:{
      //     text:'درخواست با موفقیت انجام شد',
      //     duration:8000
      //   }
      // });
    },
    err => this._snackBar.openFromComponent(ToastComponent, {
      data: {
        text: err.error,
        duration: 8000
      }
    })
  );
   }

// }

  reserveArea() {
    this.invoiceModel = {
      startDate: this.timeModel.startDate,
      endDate: this.timeModel.endDate,
      userName: this.timeModel.userName,
      svgId: this.svgId,
      spaceId: this.model.spaceId,
      amount: '123580000',
      title: this.model.title,
      typeString: this.model?.typeString ?? '',
    };

    if (this.authService.isLoggedIn()) {
      console.log('khodet inja nisti' + this.username)
      this.postReserveModel();
      this.nextStep();
    } else {
      console.log('else');
      this.nextStep();

      // const dialogRef = this.dialog.open(loginComponent, {
      //   height: '400px',
      //   width: '600px',
      // });
      // dialogRef.afterClosed().subscribe(
      //   (result) => {
      //     if (result === 'success')
      //       this.postReserveModel();
      //   });
    }
  }


  postReserveModel() {
    this.reserveModel.spaceId = this.model?.spaceId;
    this.reserveModel.svgId = this.svgId;
    this.reserveModel.userName =this.username;
   // console.log(this.reserveModel)
    //this.areaService.reserveArea(this.reserveModel).subscribe(
    //  (nxt:any) => {
      //  console.log(nxt)
       // this.router.navigateByUrl('/payment', { state: this.invoiceModel })
        // this._snackBar.openFromComponent(ToastComponent, {
        //   data: {
        //     text: 'درخواست با موفقیت انجام شد',
        //     duration: 8000
        //   }
        // });
     // },
    //  (err:any) => this._snackBar.openFromComponent(ToastComponent, {
     //   data: {
     //     text: err.error,
      //    duration: 8000
      //  }
    //  })

   // );
  }




  // login
  login() {
    this.authService.login(this.loginForm.value)
      .subscribe((nxt: any) => {
        this.localStorage.store('utid', nxt.token);
        this.localStorage.store('fullName', nxt.fullName);
        this.localStorage.store('uid', nxt.id);
        this.user = true;
        // this.router.navigate(['/payment']);
        console.log('im in the login hoorayyyyyyyyy')

      }, err => this._snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
      )
  }


 // showSignup() {
    // const dialogRef = this.dialog.open(SignupComponent, {
    //   height: '400px',
    //   width: '600px',
    // });
    // dialogRef.afterClosed().subscribe((result) => { });
   // this.next.emit();
   //this.stepper.next();

  //}

  //signup
  signup() {
    this.signupForm.markAllAsTouched();
    if (!this.signupForm.valid)
      return;
    this.authService.signup(this.signupForm.value)
      .subscribe(() => {
        this.phoneNumber = this.signupForm.get('phoneNumber')?.value;
        this.localStorage.store('phoneNumber', this.phoneNumber)
        this.localStorage.store('fullName', this.signupForm.get('firstName')?.value + " " + this.signupForm.get('lastName')?.value)
        console.log('im in the login hoorayyyyyyyyy')
        // this._snackBar.openFromComponent(ToastComponent, {
        //   data: {
        //     text: 'درخواست با موفقیت انجام شد'
        //   }
        // });
        //this.openCodeDialog()
        this.stepper.next()
      }, err => this._snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
      );
  }



  // openCodeDialog() {
  //   const dialogRef = this.dialog.open(VerificationCodeComponent, {
  //     height: '400px',
  //     width: '600px',
  //   });
  //   dialogRef.afterClosed().subscribe((result) => { });
  // }


//code
  submitCode(): void {
    this.codeForm.markAllAsTouched();
    if (!this.codeForm.valid) { return; }
    this.phoneNumber = this.localStorage.retrieve('phoneNumber')
    this.codeForm.patchValue({ phoneNumber: this.phoneNumber, verificationCode: this.codeForm.get('verificationCode')?.value })
    this.authService.submitCode(this.codeForm.value)
      .subscribe((nxt: any) => {
        this.localStorage.store('utid', nxt.token);
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
