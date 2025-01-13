import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent {
  accountService = inject(AccountService);//prima era privato togliendo il private la variabiel loggedIn non serve più
  private router = inject(Router);
  // loggedIn = false;
  //spiegazione [(ngModel)] 46: biding bidirezionale [()] se cambio da ts si modifica in html e viceversa
  model: any = {};

  login() {
    this.accountService.login(this.model).subscribe({
      next: (response) => {
        // this.loggedIn = true;
        this.router.navigateByUrl('/members'); //navigateByUrl in realtà ritorna una promise e si puo decidere cosa fare se ritorna true o false
      },
      error: (error) => console.log(error),
    });
  }

  logout(){
    // this.loggedIn = false;
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
