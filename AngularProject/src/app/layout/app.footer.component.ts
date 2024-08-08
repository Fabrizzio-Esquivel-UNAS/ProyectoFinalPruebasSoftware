import { Component, OnInit, inject } from '@angular/core';
import { LayoutService } from "./service/app.layout.service";
import { Observable, defer, first, map } from 'rxjs';
import { AccountState } from '@state/account.state';
import { AccountService } from '../shared/services/account.service';

@Component({
    selector: 'app-footer',
    templateUrl: './app.footer.component.html'
})
export class AppFooterComponent {
    accountService = inject(AccountService);
    languages: any[] = [
        { label: 'Español', value: { id: 1, name: 'Spanish', code: 'ES' } },
        { label: 'English', value: { id: 2, name: 'English', code: 'EN' } },
        { label: 'Português', value: { id: 3, name: 'Portuguese', code: 'PT' } },
        { label: 'Kechua', value: { id: 4, name: 'Quechua', code: 'QU' } },
    ];
    selectedLanguage: any;
    accountState = inject(AccountState);
    constructor(public layoutService: LayoutService) {
        this.accountState.stateItem$.pipe(
            first((user) => {
                return (user != null)
                    && (user.preferencias != null)
                    && (user.preferencias?.lenguaje != null)
            }),
            map((user) => {
                return user?.preferencias?.lenguaje
            })
        ).subscribe((lenguaje) => {
            for (let i = 0; i < this.languages.length; i++) {
                let lang = this.languages[i].value;
                if (lang.code.toLowerCase() == lenguaje) {
                    this.selectedLanguage = lang;
                    break
                }
            }
        });
    }

    onChange(event: any) {
        this.accountService.setLenguaje(event.value.code);
    }
}
