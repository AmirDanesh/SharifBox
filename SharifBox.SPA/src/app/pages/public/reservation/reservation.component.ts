import { AuthService } from './../../../shared/services/auth.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AreaService } from './../../../shared/services/area.service';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { DropdownModel } from 'src/app/shared/models/dropdown.model';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { ToastComponent } from 'src/app/shared/components/toast/toast.component';
import { MatStepper } from '@angular/material/stepper';
import { MatDialog } from '@angular/material/dialog';
import { Params, Route, Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'ngx-webstorage';
import { PaymentAreaModel } from 'src/app/shared/models/space/paymentArea.model';
import * as moment from 'jalali-moment';
@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.scss']
})
export class ReservationComponent implements OnInit {
  source: LocalDataSource = new LocalDataSource();
  typeDropDown: DropdownModel[] = [];
  // model: any = {};
  areaForm: FormGroup = new FormGroup({});
  spaceId: any;

  @Input() svgId = '';
  reserveModel: any = {};
  timeModel: any;
  // invoiceModel: InvoiceAreaModel | undefined;
  model: any;
  username: any;
  user: any;
  svgIds = ['svg_1', 'svg_2', 'svg_3', 'svg_4']
  @ViewChild('stepper') stepper: MatStepper;
  disable: any;
  data: any;
  showRegister = false;
  showLogin = false;
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
  myObj = [{
    "title": "اتاق ",
    "location": "طبقه دوم",
    "facilities": "عدد ویدیو پروژکتور",
    "properties": "مساحت 30متر"
  }];

  id: any;
  // time: any;
  // hour: any;
  // minute: any;
  // currTime: any;
  // dt: any;
  // endTime: any;
  // endHour: any;
  // endMinute: any;
  // year: any;
  // month: any;
  // currDate: any;
  // date: any;
  // m: any;
  // h:any;
  // endDate: any;

  constructor(
    private areaService: AreaService,
    private localStorage: LocalStorageService,
    private router: Router,
    public dialog: MatDialog,
    private fb: FormBuilder,
    private _snackBar: MatSnackBar,
    private authService: AuthService,
    private route: ActivatedRoute) {
    // this.source = new LocalDataSource(this.sdata);

    // this.m = moment.from('01/1989/24', 'MM/YYYY/DD');
    // console.log(this.m);
    //  console.log(this.m.format('jYYYY/jMM/jDD'));
    // this.time = new Date();
    // this.year = this.time.getFullYear()
    // this.month = this.time.getMonth()
    // this.date = this.time.getDate()
    // this.currDate = this.year + '/' + this.month + '/' + this.date;
    // //console.log(this.currDate)
    // this.hour = this.time.getHours()
    // this.minute = this.time.getMinutes()
    // this.currTime = this.hour + ':' + this.minute;



    // this.m = moment(this.currDate, 'YYYY/M/D');
    // this.h = moment(this.currTime, 'h:m');


    // this.endDate = this.m.add(2, 'month').locale('fa').format('YYYY/MM/DD');
    // this.endTime = this.m.add(2, 'hour').locale('fa').format('h:m');
   // console.log(this.endTime);

    this.source = new LocalDataSource();


    const phoneNumberPattern = '^09\\d{9}$';

    // login
    this.loginForm = new FormGroup({
      username: new FormControl(null, [Validators.required, Validators.pattern(phoneNumberPattern)]),
      password: new FormControl(null, [Validators.required, Validators.pattern('(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$')])
    })

    //signup
    this.signupForm = new FormGroup({
      firstName: new FormControl(null, [Validators.required, Validators.pattern('^[a-zA-Zآ-ی ء چ]+$')]),
      lastName: new FormControl(null, [Validators.required, Validators.pattern('^[a-zA-Zآ-ی ء چ]+$')]),
      phoneNumber: new FormControl(null, [Validators.required, Validators.pattern(phoneNumberPattern)]),
    });

    //code
    this.codeForm = new FormGroup({
      phoneNumber: new FormControl(),
      verificationCode: new FormControl(null, [Validators.required, Validators.pattern('^[0-9]{6}$')])
    });
  }

  settings = {
    hideSubHeader: false,
    noDataMessage: 'موردی ثبت نشده است',
    filter: {
      inputClass: 'filter-input',
    },
    actions: {
      columnTitle: '',
      position: 'right',
      add: false,
      edit: false,
      delete: false,
      custom: [
        {
          name: 'editAction',
          title: '<i class="fad fa-book"></i>',
        }
      ]
    },
    columns: {
      idx: {
        title: 'ردیف',
        type: 'text',
        filter: false,
        width: '30px',
        editable: false,
        addable: false,
        valuePrepareFunction: (value: any, row: any, cell: { row: { index: number; }; }) => {
          const paging = this.source.getPaging();
          return cell.row.index + (paging.page - 1) * paging.perPage + 1;
        },
      },
      title: {
        title: 'نوع'
      },
      location: {
        title: 'موقعیت'
      },
      facilities: {
        title: 'امکانات'
      },
      properties: {
        title: 'مشخصات'
      }
    }
  };

  ngOnInit(): void {

    this.route.params.subscribe(
      (params: Params) => {
        this.id = parseInt(params.id);
        // console.log(this.id)
      }
    );




    this.user = this.authService.isLoggedIn();

    this.areaForm = this.fb.group({
      username: [],
      type: [this.id, Validators.required],
      startDate: [, Validators.required],
      endDate: [],
      startTime: [],
      endTime: []
    });


    this.areaService.getTypeForReserve()
      .subscribe(
        nxt => {
          this.typeDropDown = nxt;
          //console.log(this.typeDropDown)
        });


  }

  nextStep() {
    this.stepper.next();
  }

  toggleRegister() {
    this.showRegister = !this.showRegister;
  }


  onEdit(event: any) {
    this.spaceId = event.data.spaceId;
    this.nextStep()

    this.areaService.getSpace(this.spaceId).subscribe(
      nxt => {
        console.log(nxt)
        this.model = {
          type: this.areaForm.controls.type.value,
          startDate: this.areaForm.controls.startDate.value,
          endDate: this.areaForm.controls.endDate.value,
          startTime: this.areaForm.controls.startTime.value,
          endTime: this.areaForm.controls.endTime.value,
          userName: this.phoneNumber,
          spaceId: this.spaceId,
          amount: '123580000',
          area: nxt.area,
          typeString: nxt.typeString,
          capacity: nxt.capacity,
          numOfChairs: nxt.numOfChairs,
          numOfMicrophone: nxt.numOfMicrophone,
          numOfVideoProjector: nxt.numOfVideoProjector,
          title: nxt.title
        }
      }
    )
    console.log(this.model)
  }

  submitAreaForm() {
    // this.areaForm.markAllAsTouched();
    if (!this.areaForm.valid)
      return;
    this.areaService.chooseArea(this.areaForm.value).subscribe(
      nxt => this.source.load(nxt)
    );
    console.log(this.areaForm.value)
  }

  // login
  login() {
    this.authService.login(this.loginForm.value)
      .subscribe((nxt: any) => {
        console.log(nxt)
        this.localStorage.store('utid', nxt.token);
        this.localStorage.store('fullName', nxt.fullName);
        this.localStorage.store('uid', nxt.id);
        this.phoneNumber = this.loginForm.get('username')?.value;
        this.localStorage.store('phoneNumber', this.phoneNumber);
        this.model.userName = this.phoneNumber;
        this.user = true;
        // this.router.navigate(['/payment']);
        this.authService.userSubject.next({ fullName: nxt.fullName })

      }, err => this._snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
      )
  }

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
        this.model.userName = this.phoneNumber;
        console.log(this.model)
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



  //code
  submitCode(): void {
    this.codeForm.markAllAsTouched();
    if (!this.codeForm.valid) { return; }
    this.phoneNumber = this.localStorage.retrieve('phoneNumber')
    this.codeForm.patchValue({
      phoneNumber: this.phoneNumber,
      verificationCode: this.codeForm.get('verificationCode')?.value
    })
    this.authService.submitCode(this.codeForm.value)
      .subscribe((nxt: any) => {
        this.model.userName = this.phoneNumber;
        console.log(this.model.userName)
        console.log(this.model)
        this.localStorage.store('utid', nxt.token);
        this.authService.userSubject.next({
          fullName: `${this.signupForm.get('firstName')?.value} ${this.signupForm.get('lastName')?.value}`
        })
        this._snackBar.openFromComponent(ToastComponent, {
          data: {
            text: 'درخواست با موفقیت انجام شد'
          }
        });
        //this.router.navigate(['/payment'])
        this.nextStep()
      }, err => this._snackBar.openFromComponent(ToastComponent, {
        data: {
          text: err.error
        }
      })
      );
  }




}


