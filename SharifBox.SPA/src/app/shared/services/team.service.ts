import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TeamModel } from '../models/team.model';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  baseurl = environment.apiUrl + '/Teams';
  constructor(private http: HttpClient) { }

  getTeams(): Observable<any> {
    return this.http.get(`${this.baseurl}`);
  }

  addTeam(model: TeamModel): Observable<any> {
    return this.http.post(`${this.baseurl}`, model);
  }

  getSelectedTeam(id: any): Observable<any> {
    return this.http.get(`${this.baseurl}/${id}`);
  }

  updateTeam(model: TeamModel, id: any): Observable<any> {
    console.log(id);
    return this.http.put(`${this.baseurl}/${id}`, model);
  }


}
