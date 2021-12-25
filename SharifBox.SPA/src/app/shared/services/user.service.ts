import { DropdownModel } from 'src/app/shared/models/dropdown.model';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { profileModel } from '../models/users/profile.model';
import { LocalStorageService } from 'ngx-webstorage';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseurl = environment.apiUrl + '/Users';
  uid: string;

  constructor(private http: HttpClient, private localstorage: LocalStorageService) {
    this.uid = this.localstorage.retrieve('uid');
  }

  submitProfile(model: profileModel): Observable<any> {
    model.identityUserId = this.uid;
    return this.http.put(`${this.baseurl}/${this.uid}`, model);
  }

  getUserList(): Observable<DropdownModel[]> {
    return this.http.get<DropdownModel[]>(`${this.baseurl}/dropdown`);
  }

  getSelectedUser(username: any): Observable<any> {
    return this.http.get(`${this.baseurl}/${username}`);
  }

  getَAllUser(): Observable<any> {
    return this.http.get(`${this.baseurl}/all`);
  }
}
