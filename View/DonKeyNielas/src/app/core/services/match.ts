import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ICreateMatch } from '../models/ICreateMatch.model';
import { IMatchWeekConfig } from '../models/IMatchWeekConfig.model';
import { ICompleteMatch } from '../models/ICompleteMatch.model';
import { IMatchDto } from '../models/IMatchDto.model';
import { MatchMapper } from '../mappers/match.mapper';
import { IMatchGroup } from '../models/IMatchGroup.model';

@Injectable({
  providedIn: 'root',
})
export class Match {
  private API = "http://localhost:5262/api/matches"
  constructor(private http: HttpClient) { }

  createMatches(data: ICreateMatch): Observable<boolean> {
    return this.http.post<boolean>(
      `${this.API}`,
      data
    )
  }
  getMatches(data: IMatchWeekConfig): Observable<ICompleteMatch[]> {

    return this.http.get<IMatchDto[]>(this.API, {
      params: data as any
    })
      .pipe(
        map(res => MatchMapper.fromDtoList(res))
      );
  }

  setGroupMatchResult(data: IMatchGroup){
    return this.http.put<boolean>(
      `${this.API}/group-result`,
      data
    )
  }
}
