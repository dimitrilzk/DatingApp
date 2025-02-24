import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { MemberCardComponent } from '../member-card/member-card.component';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css',
})
export class MemberListComponent implements OnInit {
  memberService = inject(MembersService);

  ngOnInit(): void {
    //questo refactor serve per evitare tante chiamate al back end ep 116
    if (this.memberService.members().length === 0) {
      this.loadMembers();
    }
  }

  loadMembers() {
    this.memberService.getMembers();
  }
}
