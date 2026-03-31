import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ITeam } from '../models/ITeam.model';

@Injectable({
  providedIn: 'root',
})
export class Teams {
   private API = "http://localhost:5262/api/team";

  constructor(private http: HttpClient) { }

  getTeams(): Observable<ITeam[]>{
    return this.http.get<ITeam[]>(
      `${this.API}`
    )
  }
}
