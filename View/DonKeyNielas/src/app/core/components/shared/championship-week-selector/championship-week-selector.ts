import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IChampionship } from '../../../models/Ichampionship.model';

@Component({
  selector: 'app-championship-week-selector',
  imports: [CommonModule, FormsModule],
  templateUrl: './championship-week-selector.html',
  styleUrl: './championship-week-selector.css',
})
export class ChampionshipWeekSelector {

  @Input() championships: IChampionship[] = [];

  @Input() selectedChampionship: number | null = null;
  @Input() selectedWeek: number = 1;
  @Input() matchWeekReadOnly: boolean = false;

  @Output() championshipChanged = new EventEmitter<number>();
  @Output() weekChanged = new EventEmitter<number>();

  weeks: number[] = [];
  ngOnChanges(changes: SimpleChanges): void {
 if (changes['selectedWeek']) {
      this.generateWeeks(changes['selectedWeek'].currentValue);
    }
  }

  private generateWeeks(totalWeeks: number) {
    this.weeks = Array.from({ length: totalWeeks }, (_, i) => i + 1);
  }
  onChampionshipChange(value: number) {
    this.selectedChampionship = value;
    this.championshipChanged.emit(value);
  }

  onWeekChange(value: number) {
    this.selectedWeek = value;
    this.weekChanged.emit(value);
  }

}
