import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-button-module',
  imports: [],
  templateUrl: './button-module.html',
  styleUrl: './button-module.css',
})
export class ButtonModule {

  @Input() buttonTitle: string = '';

  @Output() buttonClick = new EventEmitter<void>();

  onClick() {
    this.buttonClick.emit();
  }

}