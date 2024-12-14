import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [RouterOutlet, NavComponent]
})
export class AppComponent implements OnInit {
  //questo è un altro modo di fare la dep injection(alternativo al farlo nel costruttore)
  http = inject(HttpClient);
  title = 'DatingApp';
  users: any;
  
  //fare la dep inj nel costruttore è diventato antiquato 
  //angular si sposta sempre più alla programmazione funzionale cit.
  // constructor(private httpClient: HttpClient){}

  ngOnInit(): void {
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
