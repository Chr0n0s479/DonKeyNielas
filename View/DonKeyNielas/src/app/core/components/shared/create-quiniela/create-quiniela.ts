import { Component, Input } from '@angular/core';
import { IMatchDto } from '../../../models/IMatchDto.model';
import { IMatchCreateQuiniela } from '../../../models/IMatchCreateQuiniela.model';
import { MatchForecast } from "../match-forecast/match-forecast";
import { IMatchForecast } from '../../../models/IMatchForecast.model';
import { Quiniela } from '../../../services/quiniela';
import Swal from 'sweetalert2';
import { IQuiniela } from '../../../models/IQuiniela.model';
import { IQuinielaCreate } from '../../../models/IQuinielaCreate.model';

@Component({
  selector: 'app-create-quiniela',
  imports: [MatchForecast],
  templateUrl: './create-quiniela.html',
  styleUrl: './create-quiniela.css',
})
export class CreateQuiniela {
  @Input() matches: IMatchForecast[] = [];
  @Input() championshipId: number | null = null;
  @Input() matchWeek: number | null = null;
  @Input() readonly: boolean = false;

  constructor(private quinielaService: Quiniela) { }

  isCreateQuinielaOpen = false;
  toggleCreateQuiniela() {
    this.isCreateQuinielaOpen = !this.isCreateQuinielaOpen;
  }

  save() {

    if (!this.championshipId)
      return
    if (!this.matchWeek)
      return
    if (this.matches.find(m => m.forecast == null))
      return // TODO Agregar mensajes correctos.

    Swal.fire({
      title: 'Registrando quiniela...',
      allowOutsideClick: false,
      didOpen: () => {
        Swal.showLoading();
      }
    });
    console.log(this.matches)
    const data: IQuinielaCreate = {
      championshipId: this.championshipId,
      matchWeek: this.matchWeek,
      forecasts: this.matches.map(m => ({
      matchId: m.matchId,
      forecast: m.forecast!
  }))
    }
    console.log(data)
    this.quinielaService.createQuinielaForecast(data)
    .subscribe({
      next: () => {
        Swal.close();
        Swal.fire({
          icon: 'success',
          title: 'Quiniela creada',
          text: 'Quiniela creada con exito',
          timer: 1500,
          showConfirmButton: false
        })
        this.readonly = true;
      },
      error: (err) => {
        Swal.close(),
        Swal.fire({
          icon: 'error',
          title: 'Error al registrar quiniela',
          text: `${err.error}`,
          timer: 1500,
          showConfirmButton: false
        })
      }
    });

  }

  clearForecasts() {
    this.matches.forEach(m => m.forecast = null);

  }
}
