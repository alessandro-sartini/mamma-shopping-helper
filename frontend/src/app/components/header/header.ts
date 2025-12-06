import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [],
  templateUrl: './header.html',
  styleUrls: ['./header.css']
})
export class Header {
  @Input() userName: string = '';
  @Output() changeUserRequested = new EventEmitter<void>();

  onChangeUser(): void {
    this.changeUserRequested.emit();
  }
}
