import { Router } from '@angular/router';
import { UserService } from 'src/app/shared/services/user.service';
import { Component, OnInit } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  source: LocalDataSource = new LocalDataSource();
username:string;
  constructor(private userService:UserService,
               private router:Router) { }
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
          title: '<i class="fad fa-edit"></i>',
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
      phoneNumber: {
        title: ' شماره تلفن'
      },
      firstName: {
        title: ' نام'
      },
      lastName: {
        title: 'نام خانوادگی '
      },
      joinDate: {
        title: ' تاریخ عضویت '
      }
    }
  };
  ngOnInit(): void {
    this.userService.getَAllUser().subscribe(
      (nxt: any) => {
        this.source.load(nxt);
      });
  }


  onEdit(event: any) {
    this.username =event.data.phoneNumber;
    console.log(this.username);
     this.userService.getSelectedUser(this.username);
    this.router.navigate(['admin', 'user-page', this.username]);
  }
}
