import { Component } from '@angular/core';
import { ButtonModule } from "../../core/components/shared/button-module/button-module";
import { AddChampionshipModal } from '../../core/components/admin/add-championship-modal/add-championship-modal';
import { AddRoundModal } from '../../core/components/admin/add-round-modal/add-round-modal';
import { UpdateScoresModal } from "../../core/components/admin/update-scores-modal/update-scores-modal";

@Component({
  selector: 'app-admin',
  imports: [ButtonModule, AddChampionshipModal, AddRoundModal, UpdateScoresModal],
  templateUrl: './admin.html',
  styleUrl: './admin.css',
})
export class Admin {

  showChampionshipModal = false;
  showRoundModal = false;
  showResultsModal = false;

  openChampionshipModal() {
    this.showChampionshipModal = true;
  }

  openRoundModal() {
    this.showRoundModal = true;
  }

  openResultsModal() {
    this.showResultsModal = true;
  }
}
