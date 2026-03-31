import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { ITeam } from '../../../models/ITeam.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-match-row',
  imports: [CommonModule, FormsModule],
  templateUrl: './add-match-row.html',
  styleUrl: './add-match-row.css',
})
export class AddMatchRow {

  @Input() teams: ITeam[] = [];

  @Input() homeTeamId: number | null = null;
  @Input() visitTeamId: number | null = null;

  @Output() homeTeamIdChange = new EventEmitter<number | null>();
  @Output() visitTeamIdChange = new EventEmitter<number | null>();
  @ViewChild('HomeSelect')
  homeSelect!: ElementRef<HTMLSelectElement>;
  
  ngAfterViewInit() {
    this.homeSelect.nativeElement.focus();
  }

  getVisitTeams() {
    return this.teams.filter(t => t.id !== this.homeTeamId);
}
  getLocalTeams() {
    return this.teams.filter(t => t.id !== this.visitTeamId);
}
}