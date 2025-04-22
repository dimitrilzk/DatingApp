import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { MemberCardComponent } from '../member-card/member-card.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { AccountService } from '../../_services/account.service';
import { UserParams } from '../../_models/userParams';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent, PaginationModule],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css',
})
export class MemberListComponent implements OnInit {
  memberService = inject(MembersService);
  accountService = inject(AccountService);
  userParams = new UserParams(this.accountService.currentUser());

  ngOnInit(): void {
    //questo refactor serve per evitare tante chiamate al back end ep 116
    if (!this.memberService.paginatedResult()) {
      this.loadMembers();
    }
  }

  loadMembers() {
    this.memberService.getMembers(this.userParams);
  }

  pageChanged(event: any) {
    if (this.userParams.pageNumbr !== event.page) {
      this.userParams.pageNumbr = event.page;
      this.loadMembers();
    }
  }
}
