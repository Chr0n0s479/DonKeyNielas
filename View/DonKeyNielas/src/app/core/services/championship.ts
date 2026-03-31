import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IChampionship } from '../models/Ichampionship.model';
import { IChampionshipMatchWeek } from '../models/IChampionshipMatch.model';

@Injectable({
  providedIn: 'root',
})
export class Championship {
  private API = "http://localhost:5262/api/championships";

  constructor(private http: HttpClient) { }

  getChampionships(): Observable<IChampionship[]>{
    return this.http.get<IChampionship[]>(
      `${this.API}`
    )
  }

  getChampionshipById(id: number): Observable<IChampionship>{
    return this.http.get<IChampionship>(
      `${this.API}/${id}`
    )
  
  
  }
  createChampionship(data: IChampionship): Observable<IChampionship>{
    return this.http.post<IChampionship>(
      `${this.API}`,
      data,
        
    )
  }

  updateChampionship(id: number, data: IChampionship): Observable<IChampionship>{
    return this.http.put<IChampionship>(
      `${this.API}/${id}`,
      data
    )
  }

  getLatestChampionshipConfig(): Observable<IChampionshipMatchWeek>{
    return this.http.get<IChampionshipMatchWeek>(
      `${this.API}/latest`
    )
  }
  getLatestChampionshipConfigById(championshipId: number): Observable<IChampionshipMatchWeek> {
  return this.http.get<IChampionshipMatchWeek>(
    `${this.API}/${championshipId}/latestByChampionship`
  );
}


}
