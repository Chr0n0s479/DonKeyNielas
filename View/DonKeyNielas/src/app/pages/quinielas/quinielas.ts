import { ChangeDetectorRef, Component, EventEmitter, NgZone, Output } from '@angular/core';
import { ButtonModule } from '../../core/components/shared/button-module/button-module';
import { ChampionshipWeekSelector } from "../../core/components/shared/championship-week-selector/championship-week-selector";
import { ITeam } from '../../core/models/ITeam.model';
import { IChampionship } from '../../core/models/Ichampionship.model';
import { Championship } from '../../core/services/championship';
import { Teams } from '../../core/services/teams';
import { Match } from '../../core/services/match';
import { CreateQuiniela } from "../../core/components/shared/create-quiniela/create-quiniela";
import { ICompleteMatch } from '../../core/models/ICompleteMatch.model';
import { IMatchCreateQuiniela } from '../../core/models/IMatchCreateQuiniela.model';
import { MatchMapper } from '../../core/mappers/match.mapper';
import { IMatchForecast } from '../../core/models/IMatchForecast.model';
import { MatchForecast } from "../../core/components/shared/match-forecast/match-forecast";
import { switchMap } from 'rxjs';
import { Quiniela } from '../../core/services/quiniela';

@Component({
  selector: 'app-quinielas',
  imports: [ChampionshipWeekSelector, CreateQuiniela],
  templateUrl: './quinielas.html',
  styleUrl: './quinielas.css',
})
export class Quinielas {
  teams: ITeam[] = [];
  championships: IChampionship[] = [];
  @Output() close = new EventEmitter<void>();
  selectedChampionshipId: number | null = null;
  selectedWeek: number = 1;
  selectedChampionshipName: string = '';
  matches: IMatchForecast[] = [];

  hasQuiniela = false;

  isCreateQuinielaOpen = false;

  constructor(
    private championshipService: Championship,
    private matchService: Match,
    private quinielaService: Quiniela,

  ) { }

  ngOnInit(): void {

    this.championshipService
      .getChampionships()
      .subscribe(res => {
        this.championships = [...res];
      });

    this.championshipService
      .getLatestChampionshipConfig()
      .subscribe(res => {

        this.selectedChampionshipId = res.championshipId;
        this.selectedWeek = res.matchWeek;
        this.selectedChampionshipName = res.championshipName;

        this.loadQuinielaContext();

      });

  }
  toggleCreateQuiniela() {
    this.isCreateQuinielaOpen = !this.isCreateQuinielaOpen;
  }

  onChangedWeek(value: number) {
    if (!value || value < 0 || value > 9)
      return // TODO Add a span to show error
    this.selectedWeek = value;

    if (!this.selectedChampionshipId || this.selectedChampionshipId < 1)
      return // TODO Add a span to show error

    this.matchService.getMatches({
      championshipId: this.selectedChampionshipId,
      matchWeek: this.selectedWeek
    }).subscribe(res => {
      console.log(res)
      this.matches = MatchMapper.fromCompleteToMatchForecastList(res);
    })

  }
  loadQuinielaContext() {

    if (!this.selectedChampionshipId || !this.selectedWeek) return;

    this.quinielaService.getMyQuiniela({
      championshipId: this.selectedChampionshipId,
      matchWeek: this.selectedWeek
    })
      .subscribe(res => {


        if (res && res.forecasts.length > 0) {

          this.matches = [...res.forecasts]
          this.hasQuiniela = true

        } else {

          this.hasQuiniela = false
          this.loadMatches()

        }

      });

  }
  loadMatches() {

    if (!this.selectedChampionshipId || !this.selectedWeek) {
      return;
    }

    this.matchService.getMatches({
      championshipId: this.selectedChampionshipId,
      matchWeek: this.selectedWeek
    })
      .subscribe(res => {
        this.matches = MatchMapper.fromCompleteToMatchForecastList(res);

      });

  }

  onChampionshipChange(value: number) {
    this.selectedChampionshipId = value;
    this.loadQuinielaContext();
  }

  onWeekChange(value: number) {
    this.selectedWeek = value;
    this.loadQuinielaContext();
  }
}
