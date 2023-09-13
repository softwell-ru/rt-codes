# SoftWell.RtCodes.Sources.Csv

[Читатель](../Abstractions/Sources/ICodesStreamReader.cs), который умеет читать коды из csv.


## Использование

```c#
services.AddCodesConversion(
    opts => 
        opts
            .AddCsvFileSource("some-path.csv") // коды из файла на диске
            .AddCsvHttpSource("https://some-url/some-path.csv") // коды, полученные по http
        );
```


## Формат

Правила формирования csv:
- заголовок обязателен
- разделитель ';'
- комментарии разрешены
- символ комментария '\#'


Пример csv с кодами:

```csv
SourceScheme;SourceCodeValue;TargetScheme;TargetCodeValue
# parties
https://hihiclub.ru/coding-schemes/partner;SOFT;some-party-scheme;111
https://hihiclub.ru/coding-schemes/partner;WELL;some-party-scheme;222
https://hihiclub.ru/coding-schemes/partner;WELL;some-other-party-scheme;333
# products
https://hihiclub.ru/coding-schemes/instrument-id;USD/RUB Fx Tod;some-product-scheme;USD_RUB_TOD
https://hihiclub.ru/coding-schemes/instrument-id;USD/RUB Fx Tod;some-other-product-scheme;USD-RUB-TOD
```