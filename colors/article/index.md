# How to Change the Colors of Your Terminal Output

> Colorize your console background, make text green, magenta, or even dimmed.

![thumb.png](No More Boring Command Lines!)

Since our first "Hello, World!" we programmers use the terminal (or console if you are on Windows). Sometimes, however, it takes years (took 8 in my case) to realize that command-line apps can be more than just black and white and that we can use it to make communication with CLI applications more enjoyable. 

Unfortunately, we can't just do something like `[green:Text in green]` and expect the text to be rendered accordingly. Today's colorization techniques date back to the late 70s, so as you might expect, they are relatively cryptic. They rely on so-called ANSI escape codes. In this article, we will demystify these codes a little and use them for various scenarios, from simple text colorization to background changes and dimming.

## Jump Start

```sh
printf '\033[33mText In Yellow\033[0m\n'
```

![Text In Yellow](text-in-yellow.png)

- `\033[33m` - Made text yellow
- `\033[0m` - Returned it to Defaults
- `\n` - Newline

## What Colors Do We Have?

```sh
PS1='\W> '
```

```sh
printf '\033[31m'
```

```sh
printf '\033[0m'
```

![](./all-foregrounds.png)

```sh
printf '\033[32m'
printf '\033[33m'
printf '\033[34m'
printf '\033[35m'
printf '\033[36m'
printf '\033[37m'
printf '\033[38m'
printf '\033[39m'
```

| Color   | Foreground | Background | Bright Foreground | Bright Background |
| ------- | ---------: | ---------: | ----------------: | ----------------: |
| Black   |         30 |         40 |                90 |               100 |
| Red     |         31 |         41 |                91 |               101 |
| Green   |         32 |         42 |                92 |               102 |
| Yellow  |         33 |         43 |                93 |               103 |
| Blue    |         34 |         44 |                94 |               104 |
| Magenta |         35 |         45 |                95 |               105 |
| Cyan    |         36 |         46 |                96 |               106 |
| White   |         37 |         47 |                97 |               107 |


## Why is `\033[33m` so Cryptic?

- `\033` - ESC character

```sh
printf '\x1b[33mText In Yellow\x1b[0m\n'
```

```sh
printf '\e[33mText In Yellow\e[0m\n'
```

Although `\e` might look the clearest, it is bash-specific and may not work in other shells, so normally `\033` or `\x1b` is used

- `[` Beginning of a command. In literature, it's called "Control Sequence Introducer" or CSI.
- `m` specifies the type of command to be color- and format-related. In literature, it's called Select Graphic Rendition or SGR.

```sh
printf 'I want to disappear\n'
```

```sh
printf '\x1b[1J'
```

```sh
clear
```

```sh
printf '\033[2J\033[H'
```

## Advanced Commands: RGB and Dimming

The most important one: 

- `0` Resets all the settings to default

So we can always get a fresh start with:

```sh
printf '\033[0m'
```

- `2` - dimmed
- `3` - italic
- `4` - underlined

```sh
printf '\033[2m'
printf '\033[3m'
printf '\033[4m'
```

- `38` - Set custom foreground colors. Has 2 modes:
  - `38;5` - 255 range. Next number: the color
  - `38;2` - RGB. Next 3 numbers: red;green;blue

```sh
printf '\033[38;5;100m'
printf '\033[38;5;101m'
printf '\033[38;5;102m'
printf '\033[38;5;103m'
printf '\033[38;5;104m'
printf '\033[38;5;105m'
printf '\033[38;5;106m'
printf '\033[38;5;107m'
printf '\033[38;5;108m'
printf '\033[38;5;109m'
printf '\033[38;5;110m'
```

![](255-rainbow.png)

```sh
printf '\033[38;2;255;165;0m'
printf '\033[38;2;245;175;0m'
printf '\033[38;2;235;185;0m'
printf '\033[38;2;225;195;0m'
printf '\033[38;2;215;205;0m'
printf '\033[38;2;210;210;0m'
printf '\033[38;2;255;255;0m'
printf '\033[38;2;0m'
```

![](orange-to-yellow.png)

Let's conclude!

## TL;DR

In this article, we've seen how and why we can use different code with `printf '\033[<COMMANDS>m'` to change the appearance of the next things that are printed to our terminal. From built-in colors within the 30-37 range to underscore and RGBs, we've taken a look at commands of various complexity and versatility.

This article is part of the [nice-shell repository](https://github.com/astorDev/nice-shell), trying to help your shell experience be nicer. Click on the [github link](https://github.com/astorDev/nice-shell) to see other goodies it has to offer, and don't hesitate to give the repository a star! ⭐

Claps for this article are also appreciated! 😊
