import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Championship } from '../../../services/championship';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-championship-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-championship-modal.html',
  styleUrl: './add-championship-modal.css',
})
export class AddChampionshipModal {

  @Output() close = new EventEmitter<void>();

  name: string = '';
  description: string = '';


  constructor(private championshipService: Championship){}

  save() {

    Swal.fire({
      title: 'Creando torneo...',
      allowOutsideClick: false,
      didOpen: () => {
        Swal.showLoading();
      }
    });

    this.championshipService.createChampionship({
      name: this.name,
      description: this.description,
      id: 0
    })
    .subscribe({
      next: res => {
        Swal.close();
        Swal.fire({
          icon: 'success',
          title: 'Torneo creado con éxito',
          timer: 1500,
          showConfirmButton: false
        });
        this.close.emit();
      },
      error: err => {
        Swal.close();
        Swal.fire({
          icon: 'error',
          title: 'Error al crear el torneo',
          text: `${err.error}`,
          timer: 1500,
          showConfirmButton: false
        })
      }

    })

    console.log({
      name: this.name,
      description: this.description
    });

    this.close.emit();

  }
}
