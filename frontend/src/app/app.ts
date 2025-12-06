import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserNameModal } from './components/user-name-modal/user-name-modal';
import { UserService } from './services/userService';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, UserNameModal],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {

  title  = 'Mamma Shopping Helper';
  showUserNameModal = false;
  currentUserName: string = '';

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    if (!this.userService.hasUserName()) {
      this.showUserNameModal = true;
    } else {
      this.currentUserName = this.userService.getUserName()!;
      console.log(`✅ Bentornato, ${this.currentUserName}!`);
    }
  }

  onUserNameSubmitted(userName: string): void {
    console.log(`📝 Nome ricevuto dal modal: ${userName}`);

    this.userService.setUserName(userName);
    this.currentUserName = userName;
    this.showUserNameModal = false; 
    console.log(` Nome salvato: ${userName}`);
  }


  changeUserName(): void {
    this.showUserNameModal = true;
  }
}
