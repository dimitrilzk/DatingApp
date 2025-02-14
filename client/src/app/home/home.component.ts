import { Component } from '@angular/core';
import { RegisterComponent } from "../register/register.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  registerMode = false;

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }

  // getUsers(){
  //   this.http.get('https://localhost:5001/api/users').subscribe({
  //     // next: () => {},
  //     next: response => this.users = response,
  //     // error: () => {},
  //     error: error => console.log(error),
  //     // complete: () => {}
  //     complete: () => console.log('Request has completed')
  //   });
  // }
}
