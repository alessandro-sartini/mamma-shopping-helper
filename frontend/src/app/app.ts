// src/app/app.ts
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserService } from './../../services/user';
import { UserNameModal } from './components/user-name-modal/user-name-modal';
import { Header } from './components/header/header';  // ⬅️ IMPORT HEADER

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, UserNameModal, Header],  // ⬅️ AGGIUNGI Header
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App implements OnInit {
  showUserNameModal = false;
  currentUserName: string = '';

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    if (!this.userService.hasUserName()) {
      this.showUserNameModal = true;
    } else {
      this.currentUserName = this.userService.getUserName()!;
    }
  }

  onUserNameSubmitted(userName: string): void {
    this.userService.setUserName(userName);
    this.currentUserName = userName;
    this.showUserNameModal = false;
  }


  onChangeUserRequested(): void {
    this.showUserNameModal = true;
  }
}
