import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IQuiniela } from '../models/IQuiniela.model';
import { Observable } from 'rxjs';
import { IMatchWeekConfig } from '../models/IMatchWeekConfig.model';
import { IMatchForecast } from '../models/IMatchForecast.model';
import { IQuinielaCreate } from '../models/IQuinielaCreate.model';

@Injectable({
  providedIn: 'root',
})
export class Quiniela {
  private API = "http://localhost:5262/api/quinielas";

  constructor(private http: HttpClient){ }

  createQuinielaForecast(data: IQuinielaCreate): Observable<boolean>{
    console.log(data)
    return this.http.post<boolean>(
      `${this.API}`,
      data
    )
  } 

  getMyQuiniela(data: IMatchWeekConfig): Observable<IQuiniela>{
    return this.http.get<IQuiniela>(
      `${this.API}/${data.championshipId}/${data.matchWeek}/mine`
    )
  }

  
}
