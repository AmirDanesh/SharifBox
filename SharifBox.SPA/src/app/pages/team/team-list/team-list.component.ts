import { TeamService } from 'src/app/shared/services/team.service';
import { Component, OnInit } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.scss']
})
export class TeamListComponent implements OnInit {
  source: LocalDataSource = new LocalDataSource();

  constructor(private teamService:TeamService, private router:Router) { }

  settings = {
    hideSubHeader: false,
    noDataMessage: 'موردی ثبت نشده است',
    filter: {
      inputClass: 'filter-input',
    },
    add: {
      // addButtonContent: '<i class="fad fa-plus"></i>',
      // createButtonContent: '<i class="fad fa-plus"></i>',
      // cancelButtonContent: '<i class="fad fa-window-close"></i>',
      // confirmCreate: true,
    },
    actions: {
      columnTitle: 'عملیات',
      position: 'right',
      add: false,
      edit: false,
      delete: false,
      custom: [
        {
          name: 'editAction',
          title: '<i class="fad fa-edit"></i>',
        },
      ],
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
      name: {
        title: 'تیم'
      },
      managerName: {
        title: 'مسئول'
      },
      numberOfUsers: {
        title: 'تعداد اعضا',
        // valuePreparationFunction: (row, cell, value) => row.userIds.length,
      }
    }

  };

  ngOnInit(): void {
    this.teamService.getTeams().subscribe(
      (nxt: any) => {
        this.source.load(nxt);
        console.log(nxt)
      }
    );
  }


  // onAdd(event: any): void {
  //   event.confirm.resolve();
  // }



  onEdit(event: any) {
    console.log(event);
    this.teamService.getSelectedTeam(event.data.id);
    this.router.navigate(['team','',event.data.id]);
  }

}
