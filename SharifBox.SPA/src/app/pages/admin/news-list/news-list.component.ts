import { Router } from '@angular/router';
import { NewsService } from './../../../shared/services/news.service';
import { Component, OnInit } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';

@Component({
  selector: 'app-news-list',
  templateUrl: './news-list.component.html',
  styleUrls: ['./news-list.component.scss']
})
export class NewsListComponent implements OnInit {
  source: LocalDataSource = new LocalDataSource();

  constructor(private newsService: NewsService, private router: Router) {

  }

  settings = {
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
      title: {
        title: 'عنوان'
      },
      startDate: {
        title: 'تاریخ شروع'
      },
      endDate: {
        title: 'تاریخ پایان'
      }
    }
  };

  ngOnInit(): void {
    this.newsService.getNews().subscribe(
      (nxt: any) => {
        this.source.load(nxt);
      });
  }

  // onAdd(event: any): void {
  //   console.log(event);
  //   this.newsService.addNews(event.data).subscribe(nxt =>
  //     console.log(nxt)
  //   );
  //   event.confirm.resolve();
  // }
  // tslint:disable-next-line: typedef
  onEdit(event: any) {
    console.log(event);
    this.newsService.getSelectedNews(event.data.id);
    this.router.navigate(['admin', 'news', event.data.id]);
    console.log(event.data.id);
  }

  // tslint:disable-next-line: typedef
  // getSelectedNews(){
  //   this.newsService.getSelectedNews().subscribe(
  //     nxt => console.log(nxt)
  //   );
  // }
}
