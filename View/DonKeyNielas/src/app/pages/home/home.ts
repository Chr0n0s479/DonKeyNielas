import { Component } from '@angular/core';
import { Leaderboard } from '../../core/services/leaderboard';
import { ChampionshipWeekSelector } from "../../core/components/shared/championship-week-selector/championship-week-selector";
import { Championship } from '../../core/services/championship';
import { IChampionship } from '../../core/models/Ichampionship.model';
import { ShowLeaderboard } from "../../core/components/shared/show-leaderboard/show-leaderboard";
import { ILeaderboard } from '../../core/models/ILeaderboard.model';

@Component({
  selector: 'app-home',
  imports: [ShowLeaderboard, ChampionshipWeekSelector],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  championships: IChampionship[] = [];
  selectedChampionshipId: number | null = null;
  selectedWeek: number = 1;
  selectedChampionshipName: string = '';
  leaderboard: ILeaderboard | null = null;

  constructor(
    private leaderboardService: Leaderboard,
    private championshipService: Championship
  ){ }

  ngOnInit(): void{
    this.championshipService
      .getChampionships()
      .subscribe(res => {
        console.log(res)
        this.championships = [...res];
      });

    this.championshipService
      .getLatestChampionshipConfig()
      .subscribe(res => {
        console.log(res)
        this.selectedChampionshipId = res.championshipId;
        this.selectedWeek = res.matchWeek;
        this.selectedChampionshipName = res.championshipName;

        this.leaderboardService.getLeaderboard({
              championshipId: this.selectedChampionshipId!,
              matchWeek: this.selectedWeek
            }).subscribe(res => {
              this.leaderboard = res;
              
            })
      });

  }

  onChampionshipChange(value: number) {
    this.selectedChampionshipId = value;
  }

  onWeekChange(value: number) {
    this.selectedWeek = value;
  }
}
