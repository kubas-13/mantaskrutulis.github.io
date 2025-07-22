<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
  <meta http-equiv="X-UA-Compatible" content="ie=edge"/>
  <title>Mantas Krutulis Portfolio</title>
  <style>
    body {
      font-family: sans-serif;
      background: #0B132B;
      color: #ffffff;
      margin: 0;
      padding: 0;
    }

    nav {
      background: #1C2541;
      padding: 1rem 2rem;
      display: flex;
      justify-content: space-between;
      align-items: center;
      flex-wrap: wrap;
    }

    .nav-left a.name {
      color: #ffffff;
      font-family: 'Times New Roman', Times, serif;
      font-size: 32px;
      text-decoration: none;
    }

    .nav-left h2 {
      margin: 0;
      font-size: 18px;
      color: #ffffff;
    }

    .nav-right {
      display: flex;
      gap: 1.5rem;
    }

    .nav-right a {
      color: #ffffff;
      font-size: 18px;
      text-decoration: none;
    }

    .nav-right a:hover {
      color: #6FFFE9;
    }

    .about-section {
      padding: 4rem 2rem 2rem;
      max-width: 1000px;
      margin: 0 auto;
      text-align: center;
    }

    .about-section h2 {
      font-size: 42px;
      color: #5BC0BE;
      margin-bottom: 1rem;
    }

    .about-text {
      font-size: 20px;
      color: #bdcdbf;
      line-height: 1.8;
      text-align: left;
      margin-top: 2rem;
      padding: 0 1rem;
    }

    @media (max-width: 768px) {
      nav {
        flex-direction: column;
        align-items: flex-start;
      }

      .nav-right {
        margin-top: 1rem;
        flex-direction: column;
        gap: 0.5rem;
      }

      .about-text {
        text-align: center;
      }
    }
  </style>
</head>

<body>

  <nav>
    <div class="nav-left">
      <a class="name" href="PortFolio.html"><strong>Mantas Krutulis</strong></a>
      <h2>Game Developer</h2>
    </div>
    <div class="nav-right">
      <a href="PortFolio.html">HOME</a>
      <a href="Video.html">DEMOS</a>
      <a href="Game.html">GAMES</a>
      <a href="Contact.html">CONTACT</a>
    </div>
  </nav>

  <section class="about-section">
    <h2><strong>ABOUT ME</strong></h2>
    <div class="about-text">
      <p>I'm a Unity developer passionate about in-game physics and gameplay that feels smooth and responsive.</p>

      <p>Currently, I’m working on my personal project, called “BeeZit.” I’m into every part of game dev, but I mainly focus on creating fluid gameplay and integrating real-world physics into virtual systems.</p>

      <p>In the past, I’ve released four games: two hobby projects, and two from participating in game jams. I've worked on both PC and mobile titles—mostly puzzle games, with a few horror experiments thrown in. I’ve taken part in jams like Brackeys and Mini Jam: Code for a Cause.</p>

      <p>Outside of game dev, I spend time in the garage messing with electronics—anything from Arduino setups to salvaging old tech. I also enjoy solving physics and math problems, always trying to find a way to apply them in real-world or gameplay mechanics.</p>
    </div>
  </section>

</body>
</html>
