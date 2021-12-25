import { AuthService } from './../../shared/services/auth.service';
import { environment } from './../../../environments/environment';
import { Router } from '@angular/router';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { NbMenuItem, NbMenuService } from '@nebular/theme';
import { LocalStorageService, SessionStorageService } from 'ngx-webstorage';
import { filter, map } from 'rxjs/operators';
import { Route } from '@angular/compiler/src/core';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
  // providers: [NbMenuService]
})
export class LayOutComponent implements OnInit {
  fullName: string = "";
  id: string = "";

  items: NbMenuItem[] = [
    {
      title: 'مشخصات',
      icon: 'person-outline',
      link: 'profile'
    },
    {
      title: 'تغییر گذرواژه',
      icon: 'lock-outline',
      link: 'change-password'
    },
    // {
    //   title: 'Privacy Policy',
    //   icon: { icon: 'checkmark-outline', pack: 'eva' },
    // },
    {
      title: 'خروج',
      icon: 'unlock-outline',
    },
  ];

  constructor(private nbMenuService: NbMenuService,
    private localstorage: LocalStorageService,
    private sessionstorage: SessionStorageService,
    private router: Router,
    private authService: AuthService) { }

  ngOnInit(): void {

    this.authService.userSubject.subscribe(
      (nxt: any) => {
        this.fullName = nxt.fullName;
        this.id = this.localstorage.retrieve('uid')
      }
    );


    this.nbMenuService.onItemClick()
      .pipe(
        filter(({ tag }) => tag === 'my-context-menu'),
        map(({ item: { title } }) => title),
      )
      .subscribe(title => {
        if (title === 'خروج') {
          this.localstorage.clear()
          this.sessionstorage.clear()
          document.location.assign(environment.landingUrl);
        }
        if (title === 'تغییر گذرواژه') {
          this.router.navigate(['/changePass'])
        }
      });
  }
}

