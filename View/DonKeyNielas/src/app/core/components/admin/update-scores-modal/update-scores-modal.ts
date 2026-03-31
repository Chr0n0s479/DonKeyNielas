import { Component, EventEmitter, Output } from '@angular/core';
import { ChampionshipWeekSelector } from '../../shared/championship-week-selector/championship-week-selector';
import { IChampionship } from '../../../models/Ichampionship.model';
import { Championship } from '../../../services/championship';
import { Teams } from '../../../services/teams';
import { Match } from '../../../services/match';
import { ICompleteMatch } from '../../../models/ICompleteMatch.model';
import { SetMatchResults } from '../set-match-results/set-match-results';
import { IMatchScore } from '../../../models/IMatchScore.model';
import { MatchMapper } from '../../../mappers/match.mapper';
import { IMatchGroup } from '../../../models/IMatchGroup.model';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-scores-modal',
  imports: [ChampionshipWeekSelector, SetMatchResults],
  templateUrl: './update-scores-modal.html',
  styleUrl: './update-scores-modal.css',
})
export class UpdateScoresModal {

  championships: IChampionship[] = [];
  @Output() close = new EventEmitter<void>();
  selectedChampionshipId: number | null = null;
  selectedWeek: number = 1;
  matches: ICompleteMatch[] = [];
  matchCount: number = 1;
  constructor(
    private championshipService: Championship,
    private teamService: Teams,
    private matchService: Match
  ) { }


  ngOnInit(): void {

    this.championshipService
      .getChampionships()
      .subscribe(res => {
        console.log(res)
        this.championships = [...res];
      });

    this.championshipService.getLatestChampionshipConfig()
      .subscribe(res => {
        this.selectedWeek = res.matchWeek;
        this.selectedChampionshipId = res.championshipId;
        this.matchService.getMatches({
          championshipId: this.selectedChampionshipId,
          matchWeek: this.selectedWeek
        }).subscribe(res => {
          this.matches = [...res];
        })
      })


  }
  updateScoreMatch($event: IMatchScore, matchid: number) {
    if (!this.matches.find(m => m.id === matchid))
      return
    this.matches.find(m => m.id === matchid)!.scoreHomeTeam = $event.homeScore
    this.matches.find(m => m.id === matchid)!.scoreVisitTeam = $event.visitScore
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
      this.matches = [...res];
    })

  }
  matchMapper = MatchMapper;
  save() {

    if (!this.selectedChampionshipId || !this.selectedWeek) {
      console.error("Championship or week not selected");
      return;
    }
    Swal.fire({
      title: 'Actualizando resultados...',
      allowOutsideClick: false,
      didOpen: () => {
        Swal.showLoading();
      }
    });

    const data: IMatchGroup = {
      championshipId: this.selectedChampionshipId,
      matchWeek: this.selectedWeek,
      matches: this.matchMapper.fromCompleteToMatchIdList(this.matches)
    };

    this.matchService
      .setGroupMatchResult(data)
      .subscribe({
        next: () => {
          Swal.close();
          Swal.fire({
            icon: 'success',
            title: 'Resultados actualizados',
            text: 'Resultados actualizados con exito',
            timer: 1500,
            showConfirmButton: false
          });
          this.close.emit();
        },
        error: (err) => {
          Swal.close();
          Swal.fire({
            icon: 'error',
            title: 'Error al crear partidos',
            text: `${err.error}`,
            timer: 1500,
            showConfirmButton: false

          })
        }
      });

  }
}
