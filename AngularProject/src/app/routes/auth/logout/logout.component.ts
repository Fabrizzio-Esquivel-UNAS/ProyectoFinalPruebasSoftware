import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '@services/auth.service';

@Component({
  template: '',
  standalone: true,
})

export class LogoutComponent {
    authService = inject(AuthService);
    constructor(){
        this.authService.logout();
    }
}
