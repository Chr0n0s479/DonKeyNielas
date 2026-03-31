import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IMatchWeekConfig } from '../models/IMatchWeekConfig.model';
import { Observable } from 'rxjs';
import { ILeaderboard } from '../models/ILeaderboard.model';

@Injectable({
  providedIn: 'root',
})
export class Leaderboard {
  private API = "http://localhost:5262/api/leaderboard";

  constructor(private http: HttpClient){ }

  getLeaderboard(data: IMatchWeekConfig): Observable<ILeaderboard>{
    return this.http.get<ILeaderboard>(
      `${this.API}/championship/${data.championshipId}/week/${data.matchWeek}`
    )
  }
}
