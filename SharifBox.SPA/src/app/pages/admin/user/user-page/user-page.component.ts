import { profileModel } from './../../../../shared/models/users/profile.model';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/shared/services/user.service';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { AreaService } from 'src/app/shared/services/area.service';
import { Params } from '@angular/router';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.scss']
})
export class UserPageComponent implements OnInit, AfterViewInit, OnDestroy {

  constructor(private areaService:AreaService,
    private route:ActivatedRoute,
    private userService:UserService) { }
  ngOnDestroy(): void {
    this.dr.nativeElement.parentElement.parentElement.parentElement.style = 'width: 75%';
  }
  ngAfterViewInit(): void {
    this.dr.nativeElement.parentElement.parentElement.parentElement.style = 'width: 100%';

  }
  reservationSource: LocalDataSource = new LocalDataSource();
  paymentSource: LocalDataSource = new LocalDataSource();
  username:string;
  model:profileModel;

  @ViewChild('dr') dr: ElementRef;

  reservationSettings = {
    hideSubHeader: false,
    noDataMessage: 'موردی ثبت نشده است',
    filter: {
      inputClass: 'filter-input',
    },
    add: {
      // addButtonContent: '<i class="nb-plus" title="افزودن"></i>',
      // createButtonContent: '+',
      // cancelButtonContent: '-',
      // confirmCreate: true,
    },
    actions: {
      columnTitle: '',
      position: 'right',
      add: false,
      edit: false,
      delete: false,

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
          const paging = this.reservationSource.getPaging();
          return cell.row.index + (paging.page - 1) * paging.perPage + 1;
        },
      },
      type: {
        title: ' نوع فضا'
      },
      startDate: {
        title: ' تاریخ شروع'
      },
      endDate: {
        title: ' تاریخ پایان '
      },
      payAmount: {
        title: '  قیمت '
      },
      reserveDate: {
        title: ' تاریخ رزرو '
      }
    }
  };


  paymentSettings = {
    hideSubHeader: false,
    noDataMessage: 'موردی ثبت نشده است',
    filter: {
      inputClass: 'filter-input',
    },
    add: {
      // addButtonContent: '<i class="nb-plus" title="افزودن"></i>',
      // createButtonContent: '+',
      // cancelButtonContent: '-',
      // confirmCreate: true,
    },
    actions: {
      columnTitle: '',
      position: 'right',
      add: false,
      edit: false,
      delete: false,
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
          const paging = this.paymentSource.getPaging();
          return cell.row.index + (paging.page - 1) * paging.perPage + 1;
        },
      },
      amount: {
        title: '  مبلغ '
      },
      status: {
        title: ' وضعیت'
      },
      payFor: {
        title: ' پرداخت بابت '
      },
      payDate: {
        title: ' تاریخ پرداخت  '
      }
    }
  };

  ngOnInit(): void {
    this.route.params.subscribe(
      (params: Params) => {
        this.username = params.id;
      console.log(this.username)
  //this.dr.nativeElement.parentElement.parentElement.parentElement.style = 'width: 100%';
      })


    this.areaService.getReservationList(this.username).subscribe(
      (nxt: any) => {
        this.reservationSource.load(nxt);
        console.log(nxt)
      });

    this.areaService.getPaymentList(this.username).subscribe(
      (nxt: any) => {
        this.paymentSource.load(nxt);
        console.log(nxt)

      });

    this.userService.getSelectedUser(this.username).subscribe(
      (nxt: any) => {
        this.model = nxt;
        // console.log(this.model)
      }
    );



  }

}
function NativeElement(arg0: string) {
  throw new Error('Function not implemented.');
}

