import { TeamService } from '../../../shared/services/team.service';
import { Component, OnInit } from '@angular/core';
import { Params, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-team',
  templateUrl: './team-page.component.html',
  styleUrls: ['./team-page.component.scss']
})
export class TeamComponent implements OnInit {
  model: any = {};
  id: string = '';
  teamLogo:string ='';
  constructor(private teamService: TeamService, private route: ActivatedRoute) { }

  userImageUrl(userId: string) {
    return `http://demo.borhansoft.ir:5080/profilepicture/${userId}`;
  }



  ngOnInit(): void {

    this.route.params.subscribe(
      (params: Params) => {
        this.id = params.id;
        this.teamLogo = `http://demo.borhansoft.ir:5080/teamLogo/${this.id}`;
      }
    );
    this.teamService.getSelectedTeam(this.id).subscribe(
      (nxt: any) => {
        this.model = nxt;
        console.log(this.model)
      }
    );
  }

}
