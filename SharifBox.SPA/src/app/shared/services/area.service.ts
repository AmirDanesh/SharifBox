import { reserveTimeModel } from './../models/space/reserveTime.model';
import { PaymentAreaModel } from '../models/space/paymentArea.model';
import { reserveAreaModel } from '../models/space/reserveArea.model';
import { AreaModel } from '../models/space/area.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DropdownModel } from '../models/dropdown.model';
import { Observable } from 'rxjs';
import { chooseAreaModel } from '../models/space/chooseArea.model';

@Injectable({
  providedIn: 'root'
})
export class AreaService {
  // baseurlReserve = environment.apiUrl + '/Reserve/validate';
  // baseurlDate = environment.apiUrl + '/Reserve/spaceStatus';

  baseurl = environment.apiUrl + '/Spaces';
  baseurlReserve = environment.apiUrl;
  baseurlPayment = environment.apiUrl + '/Reserve/payment';

  constructor(private http: HttpClient) { }

  getSelectedArea(id: any): Observable<AreaModel> {
    return this.http.get<AreaModel>(`${this.baseurl}/bySvgId/${id}`);
  }

  addArea(model: AreaModel): Observable<any> {
    return this.http.post(`${this.baseurl}`, model);
  }

  getType(): Observable<DropdownModel[]> {
    return this.http.get<DropdownModel[]>(`${this.baseurl}/spaceTypes`);
  }

  updateArea(model: AreaModel, id: any): Observable<any> {
    return this.http.put(`${this.baseurl}/${id}`, model);
  }

  // ------------------------------------- reserve ----------------------------------------------

  // reserveTime(model: reserveTimeModel): Observable<any> {
  //   return this.http.post(`${this.baseurlDate}`, model);
  // }

  // reserveArea(model: reserveAreaModel): Observable<any> {
  //   return this.http.post(`${this.baseurlReserve}`, model);
  // }


  makePayment(model: PaymentAreaModel): Observable<string> {
    return this.http.post(`${this.baseurlPayment}`, model, { responseType: 'text' });
  }


  chooseArea(model: chooseAreaModel): Observable<any>{
    return this.http.post(`${this.baseurlReserve}/SpacesForReserve`, model);
  }

  getSpace(id:any){
    return this.http.get<AreaModel>(`${this.baseurl}/byId/${id}`);
  }

  getReservationList(username:any): Observable<any> {
    return this.http.get<any>(`${this.baseurlReserve}/userReservations/${username}`);
  }
  getPaymentList(paymentId:any): Observable<any> {
    return this.http.get<any>(`${this.baseurlReserve}/userPayments/${paymentId}`);
  }

  getTypeForReserve(): Observable<DropdownModel[]> {
    return this.http.get<DropdownModel[]>(`${this.baseurl}/spaceTypesForReserve`);
  }
}
