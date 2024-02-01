﻿using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Google.Apis.Discovery;
using static System.Net.Mime.MediaTypeNames;

public class WizardBlossomCommands : BaseCommandModule
{
    private static readonly string[] WizardBlossomResponses = {
        "Indeed, the crystal ball reveals a definite yes!",
        "Alas, the mystical energies suggest a negative response.",
        "I sense a foggy future, ask again later.",
        "The magical energies are not clear at the moment, try again.",
        "The stars align in favor, most likely!",
        "The cosmic forces deny this outcome, don't count on it.",
        "I see a magical path leading to a positive answer.",
        "The cosmic winds are uncertain, reply hazy, try again."
    };

    [Command("wizardblossom")]
    [Description("Consult Wizard Blossom to predict the future.")]
    public async Task WizardBlossomCommand(CommandContext ctx, [RemainingText] string question)
    {
        Random random = new Random();
        int index = random.Next(WizardBlossomResponses.Length);
        string response = WizardBlossomResponses[index];

        await ctx.RespondAsync($"🔮 **Question:** {question}\n**Answer:** {response}");
    }

    [Command("brewpotion")]
    [Description("Brew a potion with the cat wizard's alchemical skills.")]
    public async Task BrewPotionCommand(CommandContext ctx)
    {
        // Simulate a variety of potion ingredients
        string[] potionIngredients = { "Feline Whisker", "Stardust Catnip", "Moonbeam Essence", "Enchanted Milk", "Shadowy Purrfectness" };

        // Randomly select ingredients for the potion
        Random random = new Random();
        string ingredient1 = potionIngredients[random.Next(potionIngredients.Length)];
        string ingredient2 = potionIngredients[random.Next(potionIngredients.Length)];

        // Generate a whimsical potion name
        string potionName = $"{ingredient1} and {ingredient2} Elixir";

        // Your potion-brewing logic here

        // Simulate a brewing process (you can replace this with your own logic)
        await Task.Delay(2000); // Simulating a brewing time

        // Announce the completion of the potion
        await ctx.RespondAsync($"🧪✨ The cat wizard brews a {potionName}! It sparkles with magical energy. Drink at your own whiskerilicious risk!");
    }

    [Command("summonfamiliar")]
    [Description("Summon a magical cat familiar to aid you.")]
    public async Task SummonFamiliarCommand(CommandContext ctx)
    {
        // Simulate the summoning process (you can replace this with your own logic)
        await ctx.RespondAsync("🔮✨ Conjuring magical energies...");

        // Simulate a delay to create suspense during the summoning
        await Task.Delay(3000);

        // Announce the appearance of the familiar
        await ctx.RespondAsync("🐱✨ A magical cat familiar appears by your side. Meowgical!");
    }

    public class Spell
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
    }

        List<Spell> spells = new List<Spell>
    {
        // Level 1 Spells
        new Spell { Name = "Purrtection", Description = "Surround yourself with a soothing purr, granting advantage on Charisma saving throws and making you irresistible to feline creatures.", Level = 1 },
        new Spell { Name = "Catnip Bolt",  Description = "Hurl a magical bolt infused with the essence of catnip at a target, dealing 1d8 psychic damage. On a failed Wisdom saving throw, the target is distracted and has disadvantage on its next attack roll.", Level = 1 },
        new Spell { Name = "Mage Paw",  Description = "Transform the touched object into a small, harmless cat statue. While in this form, the object retains its properties but gains an adorable feline appearance.", Level = 1},
        new Spell { Name = "Landing on All Fours", Description = "When a creature falls, gently guide them to the ground as if they always land on their feet. The fall becomes slow and controlled, preventing fall damage.", Level = 1},
        new Spell { Name = "Furry Friends",  Description = "Summon a swarm of playful spectral cats to distract and harass enemies in the area. Creatures within the swarm's space must succeed on a Dexterity saving throw or take 1d6 slashing damage.", Level = 1},

        // Level 2 Spells
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },

        // Level 3 Spells
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },

        // Level 4 Spells
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },

        // Level 5 Spells
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },

        // Level 6 Spells
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },

        // Level 7 Spells
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },

        // Level 8 Spells
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },

        // Level 9 Spells
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },

        // Big bad boss Spells
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  },
        new Spell { Name = , Description = , Level =  }
    };

    string[] catchphrases = { "Mrowl! I am the master of magic!", "Hiss! Tremble before my arcane might!" };
    string[] castingPhrases = { "Abracadabra!", "Meowgicus!", "Purrformus!" };

    [Command("spellbook")]
    [Description("Consult the cat wizard's spellbook for arcane knowledge.")]
    public async Task SpellbookCommand(CommandContext ctx, [RemainingText] string target = null)
    {
        // Delete the command message
        await ctx.Message.DeleteAsync();

        Random random = new Random();

        // Pick a random spell
        Spell randomSpell = spells[random.Next(spells.Count)];

        // Pick a random catchphrase
        string randomCatchphrase = catchphrases[random.Next(catchphrases.Length)];

        // Pick a random casting phrase
        string randomCastingPhrase = castingPhrases[random.Next(castingPhrases.Length)];

        // Construct the message with catchphrase
        string message = $"*Evil Cat Wizard Catchphrase:* {randomCatchphrase}";

        // Send the catchphrase
        var catchphraseMessage = await ctx.RespondAsync(message);

        // Pause for 2 seconds
        await Task.Delay(2000);

        // Construct the message with casting phrase
        message = $"*Casting Phrase:* {randomCastingPhrase}";

        // Edit the catchphrase message to include casting phrase
        await catchphraseMessage.ModifyAsync(message);

        // Pause for 2 seconds
        await Task.Delay(2000);

        // Construct the final spell casting message
        message = $"**Blossom casts {randomSpell.Name}**";

        // Include target information if specified
        if (!string.IsNullOrEmpty(target))
        {
            message += $" @ {target}";
        }

        // Send the final spell casting message
        await ctx.RespondAsync(message);
    }


}
