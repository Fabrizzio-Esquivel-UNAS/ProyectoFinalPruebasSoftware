import { Component, OnInit, inject } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { AccountService } from './shared/services/account.service';
import { AuthState } from './shared/state/auth.state';
import { AuthService } from './shared/services/auth.service';
import { filter, first, switchMap, take } from 'rxjs';
import { AccountState } from '@state/account.state';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
    primengConfig = inject(PrimeNGConfig);
    authState = inject(AuthState);
    accountService = inject(AccountService);
    accountState = inject(AccountState);
    authService = inject(AuthService);

    constructor() {
        // in here, tap into authstate to watch when it becomes available,
        // and request more info from account
        // do it just once through out a session
        this.authState.stateItem$
            .pipe(
                filter((state) => {
                    return state != null
                        && state.accessToken != null;
                }),
                switchMap((state) => this.accountService.getAccount()),
            )
            .subscribe();
    }

    ngOnInit() {
        this.primengConfig.ripple = true;
    }

    title = 'AngularProject';
}
