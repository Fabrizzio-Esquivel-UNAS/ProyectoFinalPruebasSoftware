import {
  provideTransloco,
  TranslocoModule
} from '@jsverse/transloco';
import { isDevMode, NgModule } from '@angular/core';
import { TranslocoHttpLoader } from './transloco-loader';
import { TranslocoLocaleModule, provideTranslocoLocale } from '@jsverse/transloco-locale';

@NgModule({
  exports: [TranslocoModule, TranslocoLocaleModule],
  providers: [
    provideTranslocoLocale(),
    provideTransloco({
      config: {
        availableLangs: ['en', 'es', 'pt', 'qu'],
        defaultLang: 'es',
        // Remove this option if your application doesn't support changing language in runtime.
        reRenderOnLangChange: true,
        prodMode: !isDevMode(),
      },
      loader: TranslocoHttpLoader
    }),
  ],
})
export class TranslocoRootModule { }
