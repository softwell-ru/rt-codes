# SoftWell.RtCodes.Sources.Yaml

[Читатель](../Abstractions/Sources/ICodesStreamReader.cs), который умеет читать коды из yaml.


## Использование

```c#
services.AddCodesConversion(
    opts => 
        opts
            .AddYamlFileSource("some-path.yaml") // коды из файла на диске
            .AddYamlHttpSource("https://some-url/some-path.yaml") // коды, полученные по http
        );
```


## Пример yaml с кодами

```yaml
https://hihiclub.ru/coding-schemes/partner:
  SOFT:
    some-party-scheme: 111
  WELL:
    some-party-scheme: 222
    some--other-party-scheme: 333
https://hihiclub.ru/coding-schemes/instrument-id:
  "USD/RUB Fx Tod":
    some-product-scheme: USD_RUB_TOD
    some-other-product-scheme: USD-RUB-TOD
```