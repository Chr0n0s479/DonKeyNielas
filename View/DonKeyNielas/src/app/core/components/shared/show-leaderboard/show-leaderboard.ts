import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ChampionshipWeekSelector } from "../championship-week-selector/championship-week-selector";
import { ITeam } from '../../../models/ITeam.model';
import { IChampionship } from '../../../models/Ichampionship.model';
import { IMatchForecast } from '../../../models/IMatchForecast.model';
import { ILeaderboard } from '../../../models/ILeaderboard.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-show-leaderboard',
  imports: [FormsModule, CommonModule],
  templateUrl: './show-leaderboard.html',
  styleUrl: './show-leaderboard.css',
})
export class ShowLeaderboard {

  @Input() leaderboard: ILeaderboard|null = null;

}
