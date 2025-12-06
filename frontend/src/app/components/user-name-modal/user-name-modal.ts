import { Component, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-name-modal',
  standalone: true,
  imports: [ FormsModule],
  templateUrl: './user-name-modal.html',
  styleUrls: ['./user-name-modal.css']
})
export class UserNameModal {
  userName: string = '';
  errorMessage: string = '';

  
  @Output() userNameSubmitted = new EventEmitter<string>();

  onSubmit(): void {
    // Validazione
    if (!this.userName || this.userName.trim().length < 2) {
      this.errorMessage = 'Il nome deve essere almeno 2 caratteri';
      return;
    }

    this.userNameSubmitted.emit(this.userName.trim());
  }
}
