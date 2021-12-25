import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { newsModel } from './../models/news.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  baseurl = environment.apiUrl + '/Events';
  constructor(private http: HttpClient) { }

  getNews(): Observable<any> {
    return this.http.get(`${this.baseurl}`);
  }

  updateNews(model: newsModel, id: any): Observable<any> {
    return this.http.put(`${this.baseurl}/${id}`, model);
  }

  addNews(model: newsModel): Observable<any> {
    return this.http.post(`${this.baseurl}`, model);
  }

  getSelectedNews(id: any): Observable<any> {
    return this.http.get(`${this.baseurl}/${id}`);
  }
}
