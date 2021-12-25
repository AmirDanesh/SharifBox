import { LocalStorageService } from 'ngx-webstorage';
import { changePassModel as ChangePassModel } from '../models/users/changePass.model';
import { ForgotPassModel } from '../models/users/forgotPass.model';
import { CodeModel } from './../models/users/code.model';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SignupModel } from '../models/users/signup.model';
import { PassModel } from '../models/users/pass.model';
import { LoginModel } from '../models/users/login.model';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { LoginResponseModel } from '../models/users/login-response.model';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  userSubject: BehaviorSubject<any>;
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient, private localStore: LocalStorageService) {
    this.userSubject = new BehaviorSubject({ fullName: localStore.retrieve('fullName') })
  }

  isLoggedIn(): boolean {
    const token = this.localStore.retrieve('utid');
    if (!token)
      return false;
    return true;
  }

  isAdmin(): boolean {
    return false;
  }

  // first stage of signup req
  signup(model: SignupModel): Observable<string> {
    return this.http.post(`${this.baseUrl}/register`, model, { responseType: 'text' });
  }

  // second stage of signup req
  submitCode(model: CodeModel): Observable<LoginResponseModel> {
    return this.http.post<LoginResponseModel>(`${this.baseUrl}/phonenumberverify`, model);
  }

  // third stage of signup req
  submitPass(model: PassModel): Observable<string> {
    return this.http.post(`${this.baseUrl}/setpassword`, model, { responseType: 'text' });
  }

  login(model: LoginModel): Observable<LoginResponseModel> {
    return this.http.post<LoginResponseModel>(`${this.baseUrl}/login`, model);
  }

  forgotPass(model: ForgotPassModel): Observable<string> {
    return this.http.post(`${this.baseUrl}/forgetPassword`, model, { responseType: 'text' });
  }

  changePass(model: ChangePassModel): Observable<string> {
    return this.http.post(`${this.baseUrl}/changepassword`, model, { responseType: 'text' });
  }
}
