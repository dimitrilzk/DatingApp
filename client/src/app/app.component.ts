import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from './nav/nav.component';
import { AccountService } from './_services/account.service';
import { NgxSpinnerComponent } from 'ngx-spinner';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [RouterOutlet, NavComponent, NgxSpinnerComponent],
})
export class AppComponent implements OnInit {
  //questo è un altro modo di fare la dep injection(alternativo al farlo nel costruttore)
  private accountService = inject(AccountService);
  //fare la dep inj nel costruttore è diventato antiquato => constructor(private httpClient: HttpClient){}
  //angular si sposta sempre più alla programmazione funzionale cit. => http = inject(HttpClient);

  ngOnInit(): void {
    this.setCurrenUser();
  }

  setCurrenUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }
}
