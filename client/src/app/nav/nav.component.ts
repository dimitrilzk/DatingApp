import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent {
  //spiegazione [(ngModel)] 46: biding bidirezionale [()] se cambio da ts si modifica in html e viceversa
  model: any = {};

  login() {
    console.log(this.model);
  }
}
