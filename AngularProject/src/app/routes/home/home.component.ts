import { Component } from '@angular/core';
import { TranslocoModule } from '@jsverse/transloco';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
    standalone: true,
    imports: [
        TranslocoModule
    ]
})
export class HomeComponent {

}
