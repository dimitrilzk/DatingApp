import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { AccountService } from './_services/account.service';
import { HomeComponent } from "./home/home.component";

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [RouterOutlet, NavComponent, HomeComponent]
})
export class AppComponent implements OnInit {
  //questo è un altro modo di fare la dep injection(alternativo al farlo nel costruttore)
  http = inject(HttpClient);
  private accountService = inject(AccountService);
  title = 'DatingApp';
  users: any;
  
  //fare la dep inj nel costruttore è diventato antiquato 
  //angular si sposta sempre più alla programmazione funzionale cit.
  // constructor(private httpClient: HttpClient){}

  ngOnInit(): void {
    this.getUsers();
    this.setCurrenUser();
  }

  setCurrenUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

  getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe({
      // next: () => {},
      next: response => this.users = response,
      // error: () => {},
      error: error => console.log(error),
      // complete: () => {}
      complete: () => console.log('Request has completed')
    });
  }
}
