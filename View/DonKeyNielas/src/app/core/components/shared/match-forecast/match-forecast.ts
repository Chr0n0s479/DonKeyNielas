import { Component, Input } from '@angular/core';
import { IMatchCreateQuiniela } from '../../../models/IMatchCreateQuiniela.model';
import { IMatchForecast } from '../../../models/IMatchForecast.model';
import { MatchForecastEnum } from '../../../enums/MatchForecast.enum';

@Component({
  selector: 'app-match-forecast',
  imports: [],
  templateUrl: './match-forecast.html',
  styleUrl: './match-forecast.css',
})
export class MatchForecast {
  MatchForecastEnum = MatchForecastEnum;

  @Input() matches: IMatchForecast[] = [];
  @Input() readonly: boolean = false;

  setForecast(matchId: number, forecast: MatchForecastEnum) {

  const match = this.matches.find(m => m.matchId === matchId);

  if (!match)
    return;

  match.forecast = forecast;
}
}
