import { DropdownModel } from 'src/app/shared/models/dropdown.model';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SkillsService {
  baseurl = environment.apiUrl + '/skills';

  constructor(private http: HttpClient) { }

  getSkills(): Observable<any> {
    return this.http.get<DropdownModel[]>(`${this.baseurl}`);
  }

  addSkill(name: string): Observable<any> {
    return this.http.post<DropdownModel>(`${this.baseurl}`, { name });
  }
}
