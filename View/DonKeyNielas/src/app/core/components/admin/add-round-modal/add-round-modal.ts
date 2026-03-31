import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IChampionship } from '../../../models/Ichampionship.model';
import { Championship } from '../../../services/championship';
import { ChampionshipWeekSelector } from "../../shared/championship-week-selector/championship-week-selector";
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AddMatchRow } from '../add-match-row/add-match-row';
import { IMatchForm } from '../../../models/IMatchForm.model';
import { ITeam } from '../../../models/ITeam.model';
import { Teams } from '../../../services/teams';
import { Match } from '../../../services/match';
import Swal from 'sweetalert2';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-add-round-modal',
  templateUrl: './add-round-modal.html',
  styleUrl: './add-round-modal.css',
  imports: [ChampionshipWeekSelector, FormsModule, CommonModule, AddMatchRow],
})
export class AddRoundModal implements OnInit {

  teams: ITeam[] = [];
  championships: IChampionship[] = [];
  @Output() close = new EventEmitter<void>();
  selectedChampionshipId: number | null = null;
  selectedWeek: number = 1;
  matchesCount: number = 1;
  canAddMatch: boolean = true;
  matchWeenReadOnly = false;

  matches: IMatchForm[] = [
    { homeTeamId: null, visitTeamId: null }
  ];


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
        if(res.matchWeek < 17){
          this.selectedWeek = res.matchWeek + 1;
          this.matchWeenReadOnly = true;
        } else {
          this.canAddMatch = false;
        }

        this.selectedChampionshipId = res.championshipId;
      })

    this.teamService.getTeams()
      .subscribe(res => {
        console.log(res)
        this.teams = [...res]
      })
  }
  getAvailableTeams(currentMatch: IMatchForm): ITeam[] {
    const used = this.matches
      .filter(m => m !== currentMatch)
      .flatMap(m => [m.homeTeamId, m.visitTeamId])
      .filter(id => id !== null);

    return this.teams.filter(team => !used.includes(team.id));
  }
  updateMatchHome(index: number, homeId: number) {
    if (this.matches.at(index)) {
      this.matches.at(index)!.homeTeamId = homeId;
    }
  }

  updateMatchVisit(index: number, visitId: number) {
    if (this.matches.at(index)) {
      this.matches.at(index)!.visitTeamId = visitId;
    }
  }

  addMatch() {
    this.matchesCount++;
    this.matches.push({
      homeTeamId: null,
      visitTeamId: null
    });
  }

  save() {
    console.log(this.selectedChampionshipId)
    console.log(this.selectedWeek)
    if(!this.selectedChampionshipId || !this.selectedWeek)
      return
    Swal.fire({
          title: 'Creando partidos...',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          }
        });
    this.matchService.createMatches({
      championshipId: this.selectedChampionshipId!,
      matchWeek: this.selectedWeek!,
      matches: this.matches
    })
      .subscribe({
        next: res => {
          Swal.close();
          Swal.fire({
            icon: 'success',
            title: 'Partidos creados',
            text: 'Partidos creados con exito',
            timer: 1500,
            showConfirmButton: false
          });
        },
        error: err => {
          Swal.close();
          Swal.fire({
            icon: 'error',
            title: 'Error al crear partidos',
            text: `${err.error}`,
            timer: 1500,
            showConfirmButton:false

          })
        }
      })
      this.close.emit();
  }

}